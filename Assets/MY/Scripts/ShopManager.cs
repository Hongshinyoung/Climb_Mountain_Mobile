using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public Shop shop;
    public List<Item> shopItem = new List<Item>();
    public GameObject shopPrefab;
    public Transform shopItemContent;


    public void ListShopItem()
    {
        foreach(Transform item in shopItemContent)
        {
            Destroy(item.gameObject);
        }
        foreach(var item in shopItem)
        {
            GameObject obj = Instantiate(shopPrefab, shopItemContent);

            var itemName = obj.transform.Find("itemName").GetComponent<Text>();
            var itemIcon = obj.transform.Find("itemIcon").GetComponent<Image>();

            itemName.text = item.name;
            itemIcon.sprite = item.itemIcon;
        }

    }
}
