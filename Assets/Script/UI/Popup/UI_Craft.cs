using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Craft : UI_Popup
{
    enum GameObjects
    {
        CraftList,
        NeedItemList,
    }
    enum Buttons
    {
        
    }
    enum Images
    {
    }
    enum Texts
    {
        ItemName,
        ItemInfo
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

        SetCraftList();

        GetText((int)Texts.ItemName).text = "";
        GetText((int)Texts.ItemInfo).text = "";

        Transform transform = GetObject((int)GameObjects.NeedItemList).transform;

        int idx = 0;
        foreach (Transform child in transform)
        {
            child.GetComponentInChildren<TextMeshProUGUI>().text = "";
            idx++;
        }
    }

    private void SetCraftList()
    {
        GameObject go = Get<GameObject>((int)GameObjects.CraftList);

        foreach (Transform child in go.transform)
            Managers.Resource.Destroy(child.gameObject);

        for (int i = 0; i < Managers.Data.CraftItems.Count; i++)
        {
            CraftItemData cData;
            Managers.Data.CraftItems.TryGetValue(i, out cData);

            UI_CraftSlot craftSlot = Managers.UI.MakeSubItem<UI_CraftSlot>(go.transform);
            craftSlot.SetData(cData);
            craftSlot.gameObject.BindEvent(ShowCratItemInfo);
        }
    }

    private void ShowCratItemInfo(PointerEventData evt)
    {
        CraftItemData data = evt.pointerClick.GetComponent<UI_CraftSlot>().data;
        GetText((int)Texts.ItemName).text = data.ItemName;
        GetText((int)Texts.ItemInfo).text = data.ItemInfo;

        Transform transform = GetObject((int)GameObjects.NeedItemList).transform;

        int idx = 0;
        foreach(Transform child in transform)
        {
            child.GetComponentInChildren<TextMeshProUGUI>().text = data.NeedItem[idx];
            idx++;
        }
    }
}
