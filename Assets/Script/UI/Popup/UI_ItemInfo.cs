using UnityEngine.UI;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class UI_ItemInfo : UI_Popup
{
    [SerializeField] ItemBase selItem;
    enum Texts
    {
        ItemName,
        ItemDesc,
        ItemStat,
        StatValue,
        //UseTxt,
        //EquipTxt,
        //UnEquipTxt,
        //DropTxt,
    }
    enum Images
    {
        InfoBG
    }
    enum Buttons
    {
        UseBtn,
        EquipBtn,
        UnEquipBtn,
        DropBtn,
    }

    private void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();
        Bind<Image>(typeof(Images));
        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<Button>(typeof(Buttons));

        GetButton((int)Buttons.UseBtn).gameObject.BindEvent(Use);
        GetButton((int)Buttons.EquipBtn).gameObject.BindEvent(Equip);
        GetButton((int)Buttons.UnEquipBtn).gameObject.BindEvent(UnEquip);
        GetButton((int)Buttons.DropBtn).gameObject.BindEvent(Drop);
        GetImage((int)Images.InfoBG).gameObject.BindEvent(OffToggle);

        gameObject.SetActive(false);
    }


    public void SetTransform(Vector3 pos)
    {
        gameObject.transform.GetChild(0).position = pos;
    }
    public void SelectItem(ItemBase item)
    {
        selItem = item;
        SetBtn((int)Buttons.UseBtn, true);
        SetBtn((int)Buttons.EquipBtn, true);
        SetBtn((int)Buttons.UnEquipBtn, true);
        SetBtn((int)Buttons.DropBtn, true);

        GetText((int)Texts.ItemName).text = item.name +  " [" + Enum.GetName(typeof(ItemType), item.type) + "]";
        GetText((int)Texts.ItemDesc).text = item.desc;
        GetText((int)Texts.ItemStat).text = "";
        GetText((int)Texts.StatValue).text = "";
        switch(item.type)
        {
            case ItemType.Use:
                Consumables[] consumables = (item as ItemUse).consumables;
                foreach(var consumable in consumables)
                {
                    GetText((int)Texts.ItemStat).text += Enum.GetName(typeof(ItemUseStat), consumable.stat) + '\n';
                    GetText((int)Texts.StatValue).text += consumable.value.ToString() + '\n';
                }
                SetBtn((int)Buttons.EquipBtn, false);
                SetBtn((int)Buttons.UnEquipBtn, false);
                break;
            case ItemType.Weapon:
                ItemWeapon weapon = item as ItemWeapon;
                GetText((int)Texts.ItemStat).text = "[" + Enum.GetName(typeof(ItemWeaponType), weapon.weaponType) + "]";
                GetText((int)Texts.StatValue).text = "ATK: " + weapon.dmg + "\nrate: " + weapon.rate;
                SetBtn((int)Buttons.UseBtn, false);
                UI_ItemSlot itemSlot = Managers.UI.FindPopup<UI_Inventory>().GetItemSlot(item);
                if(itemSlot != null)
                {
                    SetBtn((int)Buttons.UnEquipBtn, false);
                }
                else
                {
                    SetBtn((int)Buttons.EquipBtn, false);
                    SetBtn((int)Buttons.DropBtn, false);
                }
                break;
            case ItemType.Armor:
            case ItemType.Tool:
            case ItemType.Resource:
                {
                    if (item.name.Contains("가공된"))
                    {
                        SetBtn((int)Buttons.DropBtn, false);
                    }
                    else
                        SetBtn((int)Buttons.DropBtn, true);

                    SetBtn((int)Buttons.UseBtn, false);
                    SetBtn((int)Buttons.EquipBtn, false);
                    SetBtn((int)Buttons.UnEquipBtn, false);


                    GetText((int)Texts.ItemName).text = item.name + " [" + Enum.GetName(typeof(ItemType), item.type) + "]";
                    GetText((int)Texts.ItemDesc).text = item.desc;
                    GetText((int)Texts.ItemStat).text = "";
                    GetText((int)Texts.StatValue).text = "";
                }
                break;
            default:
                break;
        }
    }

    private void OffToggle(PointerEventData evt)
    {
        Managers.UI.TogglePopupUI<UI_ItemInfo>();
        selItem = null;
    }
    private void Use(PointerEventData data)
    {
        Managers.Object.Player.Inventory.Use(selItem);
    }
    private void Equip(PointerEventData data)
    {
        Managers.Object.Player.Inventory.Equip(selItem);
    }
    private void UnEquip(PointerEventData data)
    {
        Managers.Object.Player.Inventory.UnEquip(selItem);
    }
    private void Drop(PointerEventData data)
    {
        Managers.Object.Player.Inventory.Drop(selItem);
    }
    private void SetBtn(int idx, bool status = false)
    {
        GetButton(idx).gameObject.SetActive(status);
    }
}