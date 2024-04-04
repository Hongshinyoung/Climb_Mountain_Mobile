using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public List<item> items = new List<item>();

    public Transform itemContent;
    public GameObject inventoryItem;

    private void Awake()
    {
        Instance = this;
    }

    public void Add(item item)
    {
        items.Add(item);
    }

    public void Remove(item item)
    {
        items.Remove(item);
    }

    public void ListItems()
    {
        foreach(Transform item in itemContent)
        {
            Destroy(item.gameObject);
        }

        foreach (item item in items)
        {
            GameObject obj = Instantiate(inventoryItem, itemContent);
            var itemName = obj.transform.Find("itemName").GetComponent<Text>();
            var itemIcon = obj.transform.Find("itemIcon").GetComponent<Image>();

            itemName.text = item.name;
            itemIcon.sprite = item.icon;
        }
    }
}
