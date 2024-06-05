using UnityEngine;

[CreateAssetMenu(fileName ="Item",menuName ="new ItemUse")]
public class ItemUse : ItemBase
{
    ItemUseStat[] stats;
    int[] value;

    public void UseItem()
    {

    }

}
public enum ItemUseStat
{
    None,
    Health,     // HP potion
    Stamina,    // foods
    Power,      // strength potion
    Thickness,  // def potion
}
