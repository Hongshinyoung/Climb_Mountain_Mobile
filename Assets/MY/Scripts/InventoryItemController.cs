using UnityEngine;

public class InventoryItemController : MonoBehaviour
{
    public Item item;
    public PlayerMove player;

    public void RemoveItem()
    {
        if(gameObject != null)
        {
            InventoryManager.Instance.Remove(item);
            Destroy(gameObject);
        }
        
    }
    public void UseItem()
    {
        Debug.Log("일단 useitem까진옴");
        if (item.itemName == "Juice")
        {
            Debug.Log("Juice 아이템 사용");
            player.SRun();
            
        }
        else if (item.itemName == "Banana")
        {
            Debug.Log("Banana 아이템 사용");
            player.FRun();
        }
        RemoveItem();
    }
}
