using UnityEngine;

[CreateAssetMenu(fileName ="Item",menuName ="new ItemTool")]
public class ItemTool : ItemBase, IEquippable, ICollectible
{
    public int[] targetIDs;
    public float[] collectSpeed;

    public void Collect()
    {

    }
    public void Equip()
    {
        throw new System.NotImplementedException();
    }


    public void UnEquip()
    {
        throw new System.NotImplementedException();
    }
    public void Gather()
    {
        throw new System.NotImplementedException();
    }
}
