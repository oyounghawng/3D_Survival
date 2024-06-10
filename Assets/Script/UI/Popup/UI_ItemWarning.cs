using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class UI_ItemWarning : UI_Popup
{
    enum Buttons
    {
        CloseButton
    }
    void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();
        Bind<Button>(typeof(Buttons));

        GetButton((int)Buttons.CloseButton).gameObject.BindEvent(OnCloseButton);
    }
    void OnCloseButton(PointerEventData evt)
    {
        Managers.UI.ClosePopupUI();
    }
}
