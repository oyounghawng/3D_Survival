using UnityEngine;

public class ItemData : ScriptableObject
{
    [Header("Information")]
    public int ID;
    public string name;
    public string desc;
    public ItemType type;
    public Sprite icon;
    public GameObject prefab;

    [Header("Stacking")]
    public bool canStack;
    public int maxStackAmount;
}

public enum ItemType
{
    None,
    Resource,
    Use,
    Tool,
    Weapon,
    Armor
}
