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
    enum UI_Popups
    {
        UI_Inventory,
        UI_ItemInfo
    }

    UI_Inventory inventory;
    UI_ItemInfo itemInfo;
    public Action addItem;

    private void Awake()
    {
        inventory = Managers.UI.ShowPopupUI<UI_Inventory>(Enum.GetName(typeof(UI_Popups),(int)UI_Popups.UI_Inventory));
        Managers.UI.TogglePopupUI<UI_Inventory>();
        itemInfo = Managers.UI.ShowPopupUI<UI_ItemInfo>(Enum.GetName(typeof(UI_Popups),(int)UI_Popups.UI_ItemInfo));
        Managers.UI.TogglePopupUI<UI_ItemInfo>();
        Managers.Player.Inventory = this;
        addItem += inventory.AddItem;
    }

    public void Equip(ItemBase item)
    {

    }

//    private SlotItem GetItemStack(ItemBase item)
//    {
//        foreach (SlotItem slotItem in items) 
//        { 
//        }
//    }
}
