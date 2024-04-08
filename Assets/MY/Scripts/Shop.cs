using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public Gold gold;
    public Item item;
    public InventoryManager inventoryManager;

    public void BuyItem()
    {
        inventoryManager.Add(item);
    }

    public void SellItem()
    {
        inventoryManager.Remove(item);
    }    

    void ItemFileLoad()
    {

    }
}
