using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

[CreateAssetMenu(fileName ="Item",menuName ="new ItemBase",order =0)]
public class ItemBase : ScriptableObject
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
