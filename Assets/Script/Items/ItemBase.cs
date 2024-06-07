using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;
public class ItemBase : ItemData,IInteractable
{
    public virtual string GetData()
    {
        return string.Format($"{ID} | {name} | {desc}");
    }

    public virtual void OnInteract()
    {
        Managers.Player.Inventory.addItem?.Invoke(this);
        Destroy(this);
    }

}
