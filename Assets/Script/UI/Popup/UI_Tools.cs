using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using UnityEngine.UI;


public class UI_Tools : UI_Popup
{
    enum Buttons
    {
        BuildButton,
        CraftButton,
    }

    void Start()
    {
        Init();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Managers.UI.ClosePopupUI();
        }
    }
    public override void Init()
    {
        base.Init();
        Bind<Button>(typeof(Buttons));

        GetButton((int)Buttons.BuildButton).gameObject.BindEvent(OpenBuild);
        GetButton((int)Buttons.CraftButton).gameObject.BindEvent(OpenCraft);
    }

    private void OpenBuild(PointerEventData evt)
    {
        Managers.UI.ShowPopupUI<UI_Build>();
    }
    private void OpenCraft(PointerEventData evt)
    {
        Managers.UI.ShowPopupUI<UI_Craft>();
    }

}