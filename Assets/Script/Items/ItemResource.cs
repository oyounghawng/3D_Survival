using UnityEngine;

[CreateAssetMenu(fileName ="Item",menuName ="new ItemResource")]
public class ItemResource : ItemBase
{
    [Header("Tools to collect")]
    public int collectTools;
}

