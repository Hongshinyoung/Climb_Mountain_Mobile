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
        confirmationText.text = "아이템: " + item.itemName + "\n가격: " + item.itemPrice + "골드";
        confirmationPanel.SetActive(true);
    }


    public void ConfirmPurchase()
    {
        if (currentItem != null)
        {
            BuyItem(currentItem);
            currentItem = null; // 아이템 정보 초기화
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
            // 상점에서 아이템을 구매한 후 인벤토리에 추가
            InventoryManager.Instance.Add(item);
            // 구매가 완료되면 상점 아이템 목록을 다시 표시
            ListShopItem();
        }
        else Debug.Log("골드가 부족합니다.");
       
    }

    public void SellItem(Item item)
    {
        // 인벤토리에서 아이템을 판매한 후 인벤토리 창을 업데이트
        InventoryManager.Instance.Remove(item);
        InventoryManager.Instance.ListItems();
    }
}
