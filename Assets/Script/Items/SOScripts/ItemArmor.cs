using UnityEngine;

[CreateAssetMenu(fileName ="ItemArmor",menuName = "new ItemArmor",order =1)]
public class ItemArmor : ItemBase 
{
    public ItemArmorType armtype;
    public int def;
}
public enum ItemArmorType
{
    Helmet,
    ChestPlate,
    Leggings,
    Boots,
    Shield
}
