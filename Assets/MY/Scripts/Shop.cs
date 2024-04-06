using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public Gold gold;
    public Item item;
    public InventoryManager inventoryManager;

    void BuyItem()
    {
        inventoryManager.Add(item);
    }

    void SellItem()
    {
        inventoryManager.Remove(item);
    }    
}
