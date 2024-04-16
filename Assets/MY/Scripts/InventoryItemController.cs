using UnityEngine;

public class InventoryItemController : MonoBehaviour
{
    public Item item;
    public PlayerMove player;

    public void RemoveItem()
    {
        if (gameObject != null)
        {
            InventoryManager.Instance.Remove(item);
            Destroy(gameObject);
        }

    }

    public void UseItem()
    {
        switch (item.id)
        {
            case 0:
                Debug.Log("지정된 아이템이 없습니다.");
                break;
            case 1:
                Debug.Log("바나나");
                player.SRun();
                break;
            case 2:
                Debug.Log("쥬스");
                player.FRun();
                break;

        }
        RemoveItem();
        //if (item.itemName == "Juice")
        //{
        //    Debug.Log("Juice 아이템 사용");
        //    player.SRun();
        //}
        //else if (item.itemName == "Banana")
        //{
        //    Debug.Log("Banana 아이템 사용");
        //    player.FRun();
        //}

    }
}
