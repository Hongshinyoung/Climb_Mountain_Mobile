using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Create New Item")]
public class Item : ScriptableObject
{
    public int id;
    public string itemName;
    public string description;
    public Sprite itemIcon;
    public ItemType itemType;
    public int itemPrice;

    public enum ItemType
    {
        Consumption,
        Eqiupment
    }

}
