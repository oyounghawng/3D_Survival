using UnityEngine.UI;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_ItemInfo : UI_Popup
{
    enum Texts
    {
        ItemName,
        ItemDesc,
        ItemStat,
        StatValue
    }
    enum Images
    {
        InfoBG
    }

    private void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();
        Bind<Image>(typeof(Images));
        Bind<TextMeshProUGUI>(typeof(Texts));

        GetImage((int)Images.InfoBG).gameObject.BindEvent(OffToggle);

        gameObject.SetActive(false);
    }

    public void SetTransform(Vector3 pos)
    {
        gameObject.transform.position = pos;
    }

    public void SetText(ItemBase item)
    {

    }

    private void OffToggle(PointerEventData evt)
    {
        Managers.UI.TogglePopupUI<UI_ItemInfo>();
    }
}