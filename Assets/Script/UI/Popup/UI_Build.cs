using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Build : UI_Popup
{
    enum GameObjects
    {
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
    }

    void OnCloseButton(PointerEventData evt)
    {
        Managers.UI.TogglePopupUI<UI_Inventory>();
    }
}
