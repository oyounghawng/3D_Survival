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
    }
    public UI_ItemSlot[] slots;
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

        SetInventorySlot();
        GetButton((int)Buttons.ExitBtn).gameObject.BindEvent(OnCloseButton);
        //        Managers.UI.ShowPopupUI<UI_Inventory>("UI_Inventory");
        //        Managers.UI.TogglePopupUI<UI_Inventory>();
        gameObject.SetActive(false);
    }   

    void SetInventorySlot()
    {
        GameObject go = Get<GameObject>((int)GameObjects.ItemBag);
        slots = new UI_ItemSlot[27];
        for (int i = 0; i < slots.Length; i++)
        {
            UI_ItemSlot ItemSlot = Managers.UI.MakeSubItem<UI_ItemSlot>(go.transform);
            // 정보를 전해줄 필요가 있다면 전해준다.
            ItemSlot.Idx = i;
            slots[i] = ItemSlot;
        }
    }


    void OnCloseButton(PointerEventData evt)
    {
        Managers.UI.TogglePopupUI<UI_Inventory>();
    }

    void UpdateUI()
    {
        foreach(var slot in slots)
        {
            if(slot.item)
            {
                slot.Set();
            }
            else
            {
                slot.Clear();
            }
        }
    }

    public void AddItem()
    {
        ItemBase selItem = Managers.Player.PlayerInteraction.interactGO.GetComponent<ItemObject>().item;
        if(selItem.canStack)
        {
            UI_ItemSlot slot = GetItemStack(selItem);
            if(slot is not null)
            {
                slot.quantity++;
                selItem = null;
                UpdateUI();
                return;
            }
        }
        UI_ItemSlot empty = GetEmptyItemSlot();
        if(empty != null)
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
    private UI_ItemSlot GetItemStack(ItemBase selItem)
    {
        foreach (UI_ItemSlot slot in slots)
        {
            if(slot.item == selItem && slot.quantity< selItem.maxStackAmount)
            {
                return slot;
            }
        }
        return null;
    }
    private UI_ItemSlot GetEmptyItemSlot()
    {
        foreach(UI_ItemSlot slot in slots)
        {
            if (!slot.item)
                return slot;
        }
        return null;
    }
}
