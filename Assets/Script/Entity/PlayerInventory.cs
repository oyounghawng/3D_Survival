using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
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
        itemInfo = Managers.UI.ShowPopupUI<UI_ItemInfo>(Enum.GetName(typeof(UI_Popups),(int)UI_Popups.UI_ItemInfo));
        addItem += inventory.AddItem;
    }

    public void Use(ItemBase item)
    {
        Debug.Log("PlayerInventory::Use");
        ItemUse itemUse = item as ItemUse;
        Consumables[] consumables = itemUse.consumables;
        for(int a=0; a<consumables.Length; a++)
        {
            switch(consumables[a].stat)
            {
                case ItemUseStat.Health:
                    (Managers.UI.SceneUI as UI_HUD).conditions.conditionDict[ConditionType.HP].Add(consumables[a].value);
                    break;
                case ItemUseStat.Stamina:
                    (Managers.UI.SceneUI as UI_HUD).conditions.conditionDict[ConditionType.Stamina].Add(consumables[a].value);
                    break;
            }
        }
        inventory.GetItemSlot(item).SubItem();
        inventory.UpdateUI();
        Managers.UI.TogglePopupUI<UI_ItemInfo>();
    }
    public void Equip(ItemBase item)
    {
        UI_ItemSlot slot = Managers.UI.FindPopup<UI_Inventory>().GetItemSlot(item);
        switch(item.type)
        {
            case ItemType.Weapon:
                // equipWea 이게 널이었어요
                UI_EquipSlot equipWea = inventory.GetEquipSlot(EquipType.RightHand);
                equipWea.EquipItem(item);
//                if(equip.item != null)
//                    equip = inventory.equips[(int)EquipType.LeftHand];
                break;
            case ItemType.Armor:
                UI_EquipSlot equipArm = inventory.equips[(int)(item as ItemArmor).armtype];
                equipArm.EquipItem(item);
                break;
            default:
                break;
        }
        slot.SubItem();
        Managers.UI.TogglePopupUI<UI_ItemInfo>();
    }

    public void UnEquip(ItemBase item)
    {
        UI_ItemSlot slot = Managers.UI.FindPopup<UI_Inventory>().GetEmptyItemSlot();
        if(slot == null)
        {
            Debug.Log("There's no empty slot available");
        }
        slot.item = item;
        slot.Set();

        foreach(UI_EquipSlot i in Managers.UI.FindPopup<UI_Inventory>().equips)
        {
            if (i.item == item)
            {
                i.UnEquip();
                break;
            }
        }
        Managers.UI.TogglePopupUI<UI_ItemInfo>();
    }
    public void Drop(ItemBase item)
    {
        Debug.Log("PlayerInventory::Drop");
        Managers.Resource.Instantiate($"Resource/{item.name}").transform.position = 
            Managers.UI.FindPopup<UI_Inventory>().dropPos.position + 2*Vector3.forward + 3*Vector3.up;

        UI_ItemSlot slot = Managers.UI.FindPopup<UI_Inventory>().GetItemSlot(item);
        slot.item = null;
        slot.Clear();
    }

//    private SlotItem GetItemStack(ItemBase item)
//    {
//        foreach (SlotItem slotItem in items) 
//        { 
//        }
//    }

}
