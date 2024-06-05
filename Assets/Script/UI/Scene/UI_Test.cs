using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Test : UI_Scene
{
    enum Gameobjects
    {

    }

    enum Texts
    {

    }

    enum Buttons
    {
        TestButton,
    }
    void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();

        Bind<Button>(typeof(Buttons));
        GetButton((int)Buttons.TestButton).gameObject.BindEvent(Test);
    }

    private void Test(PointerEventData evt)
    {
        Debug.Log("Å×½ºÆ®");
    }

}
