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
        Debug.Log("�ϴ� useitem������");
        if (item.itemName == "Juice")
        {
            Debug.Log("Juice ������ ���");
            player.SRun();
            
        }
        else if (item.itemName == "Banana")
        {
            Debug.Log("Banana ������ ���");
            player.FRun();
        }
        RemoveItem();
    }
}
