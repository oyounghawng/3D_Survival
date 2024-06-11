using TMPro;

public class UI_CraftSlot : UI_Base
{
    public CraftItemData data;
    enum Texts
    {
        CraftName
    }
    private void Start()
    {
        Init();
    }
    public override void Init()
    {
        Bind<TextMeshProUGUI>(typeof(Texts));
        GetText((int)Texts.CraftName).text = data.ItemName;

    }
    public void SetData(CraftItemData _data)
    {
        data = _data;
    }
}
