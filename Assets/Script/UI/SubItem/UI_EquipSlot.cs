using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_EquipSlot : UI_ItemSlot
{
    public EquipType equipType;
    enum Buttons
    {
        UI_EquipSlot,
    }
    enum Images
    {
        Icon,
        default_icon,
    }

    private void Start()
    {
        Init();
    }
    private void Init()
    {
        Bind<Image>(typeof(Images));
        Bind<Button>(typeof(Buttons));

        GetButton((int)Buttons.UI_EquipSlot).gameObject.BindEvent(OnSelect);
    }
//    public override void Set()
//    {
//        base.Set();
//    }
//    public override void Clear()
//    {
//        if (item == null)
//            return;
//        UI_ItemInfo itemInfo = Managers.UI.TogglePopupUI<UI_ItemInfo>();
//        itemInfo.SelectItem(item);
//        itemInfo.SetTransform(transform.position);
//    }

    public void EquipItem(ItemBase item)
    {
        if (item.type is not ItemType.Weapon or ItemType.Tool or ItemType.Armor)
            return;
        this.item = item;
        GetImage((int)Images.Icon).sprite = item.icon;
        GetImage((int)Images.default_icon).gameObject.SetActive(false);
    }

    public void UnEquip()
    {
        item = null;
        GetImage((int)Images.Icon).sprite = null;
        GetImage((int)Images.default_icon).gameObject.SetActive(true);
    }
}
public enum EquipType
{
    Helmet,
    ChestPlate,
    Leggings,
    Boots,
    RightHand,
    LeftHand
}