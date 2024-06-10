using System;
using UnityEngine;

[CreateAssetMenu(fileName ="ItemUse",menuName ="new ItemUse")]
public class ItemUse : ItemBase
{
    public Consumables[] consumables;
}

[Serializable]
public class Consumables
{
    public ItemUseStat stat;
    public int value;
}
public enum ItemUseStat
{
    None,
    Health,     // HP potion
    Hunger,    // foods
    Power,      // strength potion
    Thickness,  // def potion
}
