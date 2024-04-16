// ShopManager.cs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public Shop shop;
    public Gold gold;
    public List<Item> shopItem = new List<Item>();
    public GameObject shopItems;
    public Transform shopItemContent;
    public Button buyButton;
    public GameObject confirmationPanel;
    public Text confirmationText;
    private Item currentItem;

    private void Start()
    {
        confirmationPanel.SetActive(false);
    }

    public void ListShopItem()
    {
        foreach (Transform item in shopItemContent)
        {
            Destroy(item.gameObject);
        }
        foreach (var item in shopItem)
        {
            GameObject obj = Instantiate(shopItems, shopItemContent);

            var itemName = obj.transform.Find("itemName").GetComponent<Text>();
            var itemIcon = obj.transform.Find("itemIcon").GetComponent<Image>();
            var shopItemButton = obj.GetComponentInChildren<Button>();

            itemName.text = item.itemName;
            itemIcon.sprite = item.itemIcon;

            // buyButton.onClick.AddListener(() => BuyItem(item));
            shopItemButton.onClick.AddListener(() => ShowConfirmationPanel(item));

        }

    }


    public void ShowConfirmationPanel(Item item)
    {
        currentItem = item;
        confirmationText.text = "������: " + item.itemName + "\n����: " + item.itemPrice + "���";
        confirmationPanel.SetActive(true);
    }


    public void ConfirmPurchase()
    {
        if (currentItem != null)
        {
            BuyItem(currentItem);
            currentItem = null; // ������ ���� �ʱ�ȭ
            confirmationPanel.SetActive(false);
            InventoryManager.Instance.ListItems();
        }
    }


    public void CancelPurchase()
    {
        confirmationPanel.SetActive(false);
    }

    public void BuyItem(Item item)
    {
        int playerGold = gold.GetGold();
        if (playerGold >= item.itemPrice)
        {
            gold.UseGold(item.itemPrice);
            // �������� �������� ������ �� �κ��丮�� �߰�
            InventoryManager.Instance.Add(item);
            // ���Ű� �Ϸ�Ǹ� ���� ������ ����� �ٽ� ǥ��
            ListShopItem();
        }
        else Debug.Log("��尡 �����մϴ�.");
       
    }

    public void SellItem(Item item)
    {
        // �κ��丮���� �������� �Ǹ��� �� �κ��丮 â�� ������Ʈ
        InventoryManager.Instance.Remove(item);
        InventoryManager.Instance.ListItems();
    }
}
