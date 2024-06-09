using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_ItemSlot : UI_Base
{
    [SerializeField]private int idx;
    public ItemBase item;              // Item data
    public UIInventory inventory;

    public Button btn;                 // Click actions
    public Image icon;                 // icon on display
    public TextMeshProUGUI quantityTxt;// number of stacks in inventory
    private UnityAction btnAction;

    public int quantity;

    public int Idx {  get { return idx; } set { idx = value; } }
    enum Images
    {
        Icon,
    }
    private void Start()
    {
        Init();
    }
    public override void Init()
    {
        // Button.gameObject.BindEvent is same with below code
//        btnAction += OnSelect;
//        GetComponent<Button>().onClick.AddListener(btnAction);
        Bind<Image>(typeof(Images));
        gameObject.BindEvent(OnSelect);
        quantityTxt = transform.GetComponentInChildren<TextMeshProUGUI>();
        icon = transform.GetChild(0).GetComponent<Image>();
    }
    public virtual void Set()
    {
        icon.gameObject.SetActive(true);
        icon.sprite = item.icon;
        if(item.type is ItemType.Resource or ItemType.Use)
        {
            quantityTxt.gameObject.SetActive(true);
            quantityTxt.text = quantity > 0 ? quantity.ToString() : string.Empty;
        }
    }
    public virtual void Clear()
    {
        if (item == null)
        {
            icon.gameObject.SetActive(false);
            return;
        }
        if(item.type is ItemType.Resource or ItemType.Use)
            quantityTxt.text = quantity.ToString();
    }
    public void BtnDebug()
    {
        Debug.Log("Debug");
    }
    public void OnSelect(PointerEventData evt)
    {
        UI_ItemInfo itemInfo = Managers.UI.TogglePopupUI<UI_ItemInfo>();
        Debug.Log("Select");
        itemInfo.SetTransform(transform.position);
        itemInfo.SetText(item);
    }
}
