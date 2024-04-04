using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItemController : MonoBehaviour
{
    public Item item;

    public void RemoveItem()
    {
        InventoryManager.Instance.Remove(item);
        Destroy(gameObject);
    }
    public void UseItem()
    {
        switch(item.itemType)
        {
            case Item.ItemType.Consumption:
                //Player.Instance.IncreaseHp(item.value);
                Debug.Log("소비 아이템 사용");
                break;
            case Item.ItemType.Eqiupment:
                //Player.Instance.IncreaseExp(item.value);
                Debug.Log("장비 아이템 장착");
                break;
        }
        RemoveItem();
    }
}
