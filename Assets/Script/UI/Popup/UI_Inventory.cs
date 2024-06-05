using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using Unity.VisualScripting;

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
    }

    void SetInventorySlot()
    {
        GameObject go = Get<GameObject>((int)GameObjects.ItemBag);

        foreach (Transform child in go.transform)
            Managers.Resource.Destroy(child.gameObject);

        for (int i = 0; i < 27; i++)
        {
            UI_ItemSlot ItemSlot = Managers.UI.MakeSubItem<UI_ItemSlot>(go.transform);
            // 정보를 전해줄 필요가 있다면 전해준다.
        }
    }

    void OnCloseButton(PointerEventData evt)
    {
        Managers.UI.TogglePopupUI<UI_Inventory>();
    }

    void AddItem(ItemBase item)
    {

    }
}
