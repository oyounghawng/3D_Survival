using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class UI_Inventory : UI_Popup
{
    enum GameObjects
    {
        ItemBag
    }
    enum Buttons
    {
        BackBtn
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
        GetButton((int)Buttons.BackBtn).gameObject.BindEvent(OnCloseButton);
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
        Managers.UI.ClosePopupUI();
    }
}
