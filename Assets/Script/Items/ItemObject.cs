using UnityEngine;

public class ItemObject : MonoBehaviour, IInteractable
{
    public ItemBase item;

    public string GetData()
    {
        return string.Format($"{item.ID} | {item.name} | {item.desc}");
    }

    public void OnInteract()
    {
        Managers.Object.Player.Inventory.addItem?.Invoke();
        Destroy(gameObject);
    }

}
enum PromptFormat
{
    ID,
    Name,
    Description
}