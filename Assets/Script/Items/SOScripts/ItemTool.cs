using System;
using UnityEngine;

[CreateAssetMenu(fileName ="ItemTool",menuName ="new ItemTool")]
public class ItemTool : ItemBase
{
    public CollectStat[] stats;
}

[Serializable]
public class CollectStat
{
    public int targetID;
    public float collectSpeed;
}
