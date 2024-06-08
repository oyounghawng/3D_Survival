using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class UI_ItemInfo : UI_Popup
{
    enum Texts
    {
        ItemName,
        ItemDesc,
        ItemStat,
        StatValue
    }
    enum BG
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
        Bind<Image>(typeof(BG));
        Bind<TextMeshProUGUI>(typeof(Texts));
    }

    public void SetTransform(Vector3 pos)
    {
        gameObject.transform.position = pos;
    }

    public void SetText(ItemBase item)
    {

    }
}