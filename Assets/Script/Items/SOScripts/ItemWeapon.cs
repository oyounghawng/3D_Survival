using UnityEngine;

[CreateAssetMenu(fileName ="ItemWeapon",menuName ="new ItemWeapon")]
public class ItemWeapon : ItemBase 
{
    public ItemWeaponType weaponType;
    public int dmg;
    public float rate;
}
public enum ItemWeaponType
{
    None,
    Sword,
    Bow,
    Projectile
}

