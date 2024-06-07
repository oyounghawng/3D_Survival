using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;
public class ItemBase : MonoBehaviour,IInteractable
{
    private ItemData data;
    public ItemData Data
    {
        get { return data; } 
    }
    public virtual string GetData()
    {
        return string.Format($"{data.ID} | {data.name} | {data.desc}");
    }

    public virtual void OnInteract()
    {
        Managers.Player.Inventory.addItem?.Invoke(this);
        Destroy(gameObject);
    }

}
