using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Inventory : UI_Popup
{
    enum GameObjects
    {
        ItemBag,
        Equip,
        ExitBtn,
        DescPannel
    }
    enum Buttons
    {
        ExitBtn
    }
    enum Images
    {
    }
    enum Texts
    {
        PlayerLvl,
        PlayerName,
        PlayerATK,
        PlayerDEF,

    }
    public UI_ItemSlot[] slots;
    public UI_EquipSlot[] equips;
    public Transform dropPos;

    void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();
        Bind<Button>(typeof(Buttons));
        Bind<GameObject>(typeof(GameObjects));
        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<Image>(typeof(Images));

        dropPos = Managers.Object.Player.gameObject.transform;
        SetInventorySlot();
        GetButton((int)Buttons.ExitBtn).gameObject.BindEvent(OnCloseButton);
    }
    
    void SetInventorySlot()
    {
        GameObject go = Get<GameObject>((int)GameObjects.ItemBag);
        slots = new UI_ItemSlot[27];
        for (int i = 0; i < slots.Length; i++)
        {
            UI_ItemSlot ItemSlot = Managers.UI.MakeSubItem<UI_ItemSlot>(go.transform);
            ItemSlot.Idx = i;
            slots[i] = ItemSlot;
        }
        go = Get<GameObject>((int)GameObjects.Equip);
        equips = new UI_EquipSlot[Enum.GetValues(typeof(EquipType)).Length];
        for (int a = 0; a < equips.Length; a++)
        {
            UI_EquipSlot equipSlot = go.transform.GetChild(a).GetComponent<UI_EquipSlot>();
            equipSlot.Idx = a;
            equipSlot.equipType = (EquipType)a;
            equips[a] = equipSlot;
        }
    }


    void OnCloseButton(PointerEventData evt)
    {
        Managers.UI.TogglePopupUI<UI_Inventory>();
        if (Cursor.lockState != CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void UpdateUI()
    {
        foreach (var slot in slots)
        {
            if (slot.item)
            {
                slot.Set();
            }
            else
            {
                slot.Clear();
            }
        }
        ShowStatus();
    }

    public void AddItem()
    {
        ItemBase selItem = Managers.Object.Player.PlayerInteraction.interactGO.GetComponent<ItemObject>().item;
        if (selItem.canStack)
        {
            UI_ItemSlot slot = GetItemSlot(selItem);
            if (slot is not null)
            {
                slot.quantity++;
                selItem = null;
                UpdateUI();
                return;
            }
        }
        UI_ItemSlot empty = GetEmptyItemSlot();
        if (empty != null)
        {
            empty.item = selItem;
            empty.quantity = 1;
            selItem = null;
            UpdateUI();
            return;
        }
        Debug.Log("Cannot hold Item due to inventory storage");
        return;

    }
    public UI_ItemSlot GetItemSlot(ItemBase selItem)
    {
        foreach (UI_ItemSlot slot in slots)
        {
            if (slot.item == selItem && (selItem.canStack ? slot.quantity < selItem.maxStackAmount : true))
            {
                return slot;
            }
        }
        return null;
    }
    public UI_ItemSlot GetEmptyItemSlot()
    {
        foreach (UI_ItemSlot slot in slots)
        {
            if (!slot.item)
                return slot;
        }
        return null;
    }
    private void ShowStatus()
    {
        GetText((int)Texts.PlayerLvl).text = "Lvl: " + Managers.Object.Player.stat.lvl.ToString();
        GetText((int)Texts.PlayerName).text = "Name: " + Managers.Object.Player.stat.name;
        GetText((int)Texts.PlayerATK).text = "ATK: " + Managers.Object.Player.stat.ATK.ToString();
        GetText((int)Texts.PlayerDEF).text = "DEF: " + Managers.Object.Player.stat.DEF.ToString();
    }
    public UI_EquipSlot GetEquipSlot(EquipType equipType)
    {
        foreach (UI_EquipSlot slot in equips)
        {
            if (slot.equipType == equipType)
                return slot;
        }
        Debug.Log("InfiniteLoop");
        return null;
    }
}
