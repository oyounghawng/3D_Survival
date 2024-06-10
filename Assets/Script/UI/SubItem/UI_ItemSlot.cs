using TMPro;
using Unity.VisualScripting;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_ItemSlot : UI_Base
{
    [SerializeField] private int idx;
    public ItemBase item;              // Item data
    public int quantity;
    public int Idx { get { return idx; } set { idx = value; } }

    enum Buttons
    {
        UI_ItemSlot,
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

        Debug.Log("asd");
        GetImage((int)Images.Icon).sprite = null;
        GetText((int)Texts.Num).text = "";
        GetButton((int)Buttons.UI_ItemSlot)?.gameObject.BindEvent(OnSelect);
    }
    public virtual void Set()
    {
        GetImage((int)Images.Icon).gameObject.SetActive(true);
        GetImage((int)Images.Icon).sprite = item.icon;
        if (item.type is ItemType.Resource or ItemType.Use)
        {
            GetText((int)Texts.Num).gameObject.SetActive(true);
            GetText((int)Texts.Num).text = quantity > 0 ? quantity.ToString() : string.Empty;
        }

    }
    public virtual void Clear()
    {
        if (!item)
        {
            GetImage((int)Images.Icon).gameObject.SetActive(false);
            GetText((int)Texts.Num).text = string.Empty;
            return;
        }
        if (item.type is ItemType.Resource or ItemType.Use)
            GetText((int)Texts.Num).text = quantity.ToString();

    }
    public void OnSelect(PointerEventData evt)
    {
        if (item == null)
            return;
        UI_ItemInfo itemInfo = Managers.UI.TogglePopupUI<UI_ItemInfo>();
        Debug.Log("Select");
        itemInfo.SelectItem(item);
        itemInfo.SetTransform(transform.position);
    }
    public void SubItem()
    {
        quantity--;
        if(quantity <=0)
        {
            item = null;
            GetImage((int)Images.Icon).sprite = null;
            GetText((int)Texts.Num).text = "";
        }
    }
}
