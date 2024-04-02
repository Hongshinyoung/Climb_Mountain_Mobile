using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Item/Create New Item")]
public class item : ScriptableObject
{
    public int id;
    public string itemName;
    public string description;
    public Sprite icon;
}
