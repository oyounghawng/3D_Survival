using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI_HUD : UI_Scene
{
    public UI_Conditions conditions;

    // temp
    public TextMeshProUGUI promptText;
    public GameObject promptTextBG;

    enum Buttons
    {
        Craft
    }

    enum BG
    {
        PromptTextBG
    }
    enum Texts
    {
        PromptText
    }

    private void Start()
    {
        conditions = Util.FindChild<UI_Conditions>(gameObject);
        promptText = transform.GetChild(2).GetComponentInChildren<TextMeshProUGUI>();
        promptTextBG = transform.GetChild(2).gameObject;
        Init();
    }

    public override void Init()
    {
        base.Init();
        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<GameObject>(typeof(BG));
        Bind<Button>(typeof(Buttons));

//        GetText((int)Texts.PromptText).text = "";
//        Get<GameObject>((int)GameObjects.PromptTextBG).SetActive(true);
//        Debug.Log("Test");
    }

    [ContextMenu("asd")]
    private void OpenCraft()
    {
        Managers.UI.ShowPopupUI<UI_Craft>();
    }
}
