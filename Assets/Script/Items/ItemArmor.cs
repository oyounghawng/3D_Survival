using UnityEngine;

[CreateAssetMenu(fileName ="ItemArmor",menuName = "new ItemArmor",order =1)]
public class ItemArmor : ItemBase, IEquippable
{
    public ItemArmorType armtype;
    public int def;

    public void Equip()
    {
        throw new System.NotImplementedException();
    }

    public void UnEquip()
    {
        throw new System.NotImplementedException();
    }
}
public enum ItemArmorType
{
    None,
    Helmet,
    ChestPlate,
    Leggings,
    Boots,
    Shield
}
