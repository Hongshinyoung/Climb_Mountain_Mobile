using UnityEngine;
using System.Collections;

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
        RemoveItem();
        switch (item.id)
        {
            case 0:
                Debug.Log("������ �������� �����ϴ�.");
                break;
            case 1:
               
                Debug.Log("�ٳ���");   
                player.SRun();
                break;
            case 2:
                Debug.Log("�꽺");
                player.FRun();
                break;

        }


    }
}
