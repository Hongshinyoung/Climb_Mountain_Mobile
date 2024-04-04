using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    public item item;
    void PickUpItem()
    {
        InventoryManager.Instance.Add(item);
        Destroy(gameObject);
    }

    private void OnMouseDown()
    {
        PickUpItem();
    }
}
