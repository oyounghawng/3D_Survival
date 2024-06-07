interface IInteractable
{
    public string GetData();
    public void OnInteract();
}
interface IEquippable
{
    public void Equip();
    public void UnEquip();
}

interface IAttack
{
    public void Attack();
}

interface ICollectible
{
    public void Gather();
}