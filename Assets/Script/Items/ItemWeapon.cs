using UnityEngine;

[CreateAssetMenu(fileName ="Item",menuName ="new ItemWeapon")]
public class ItemWeapon : ItemBase, IEquippable, IAttack
{
    public ItemWeaponType weaponType;
    public int dmg;
    public float rate;
    public void Attack()
    {
        throw new System.NotImplementedException();
    }

    public void Equip()
    {
        throw new System.NotImplementedException();
    }

    public void UnEquip()
    {
        throw new System.NotImplementedException();
    }
}
public enum ItemWeaponType
{
    None,
    Sword,
    Bow,
    Projectile
}

