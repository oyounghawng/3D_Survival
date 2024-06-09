using System;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    enum EquipType // for equipments
    {
        Helmet,
        ChestPlate,
        Leggings,
        Boots,
        LeftHand,
        RightHand
    }

    int nitem = 30;
    ItemBase[] items; // item ids
    ItemBase[] equips; // equip ids

    public Action<ItemBase> addItem;

    private void Awake()
    {
        Managers.UI.ShowPopupUI<UI_Inventory>("UI_Inventory");
        Managers.UI.TogglePopupUI<UI_Inventory>();
        Managers.Player.Inventory = this;
        items = new ItemBase[nitem];
        equips = new ItemBase[Enum.GetValues(typeof(EquipType)).Length];
        addItem += AddItem;
    }

    public void AddItem(ItemBase item)
    {
        if(item.Data.canStack)
        {
//            SlotItem slot = GetItemStack(item);
        }
    }

//    private SlotItem GetItemStack(ItemBase item)
//    {
//        foreach (SlotItem slotItem in items) 
//        { 
//        }
//    }
}
