using UnityEngine;

[CreateAssetMenu(fileName ="ItemResource",menuName ="new ItemResource")]
public class ItemResource : ItemBase
{
    [Header("Tools to collect")]
    public int[] collectTools;
}

