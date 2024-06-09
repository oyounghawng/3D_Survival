using TMPro;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_ItemSlot : UI_Base
{
    [SerializeField] private int idx;
    public ItemBase item;              // Item data
    public UIInventory inventory;
    private UnityAction btnAction;
    public int quantity;
    public int Idx { get { return idx; } set { idx = value; } }

    enum Buttons
    {
        UI_ItemSlot
    }
    enum Images
    {
        Icon,
    }
    enum Texts
    {
        Num,
    }
    private void Start()
    {
        Init();
    }
    public override void Init()
    {
        Bind<Image>(typeof(Images));
        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<Button>(typeof(Buttons));

        GetImage((int)Images.Icon).sprite = null;
        GetText((int)Texts.Num).text = "";
        GetButton((int)Buttons.UI_ItemSlot).gameObject.BindEvent(OnSelect);

    }
    public virtual void Set()
    {
        /*
        icon.gameObject.SetActive(true);
        icon.sprite = item.icon;
        if (item.type is ItemType.Resource or ItemType.Use)
        {
            quantityTxt.gameObject.SetActive(true);
            quantityTxt.text = quantity > 0 ? quantity.ToString() : string.Empty;
        }
        */
    }
    public virtual void Clear()
    {
        /*
        if (item == null)
        {
            icon.gameObject.SetActive(false);
            return;
        }
        if (item.type is ItemType.Resource or ItemType.Use)
            quantityTxt.text = quantity.ToString();
        */
    }
    public void OnSelect(PointerEventData evt)
    {
        UI_ItemInfo itemInfo = Managers.UI.TogglePopupUI<UI_ItemInfo>();
        Debug.Log("Select");
        itemInfo.SetTransform(transform.position);
        itemInfo.SetText(item);
    }
}
