using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemBase",menuName ="new ItemBase")]
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
