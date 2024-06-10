using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class UI_Craft : UI_Popup
{
    private CraftItemData curData;
    enum GameObjects
    {
        CraftList,
        NeedItemList,
    }
    enum Buttons
    {
        CraftButton
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

        if (transform)
        {
            Debug.Log("있어용");
        }

        int idx = 0;
        foreach (Transform child in transform)
        {
            child.GetComponentInChildren<TextMeshProUGUI>().text = "";
            idx++;
        }
        GetButton((int)Buttons.CraftButton).gameObject.BindEvent(MakeCraftItem);
        GetButton((int)Buttons.CraftButton).gameObject.SetActive(false);
        if (Cursor.lockState == CursorLockMode.Locked)
            Cursor.lockState = CursorLockMode.None;
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

        curData = evt.pointerClick.GetComponent<UI_CraftSlot>().data;
        GetText((int)Texts.ItemName).text = curData.ItemName;
        GetText((int)Texts.ItemInfo).text = curData.ItemInfo;
        GetButton((int)Buttons.CraftButton).gameObject.SetActive(true);
        Transform transform = GetObject((int)GameObjects.NeedItemList).transform;

        int idx = 0;
        foreach (Transform child in transform)
        {
            child.GetComponentsInChildren<TextMeshProUGUI>()[0].text = curData.NeedItem[idx];
            idx++;
        }
        idx = 0;
        foreach (Transform child in transform)
        {
            child.GetComponentsInChildren<TextMeshProUGUI>()[1].text = curData.NeedCost[idx] == 0 ? "" : curData.NeedCost[idx].ToString();
            idx++;
        }
    }

    public bool[] IsMake;
    private void MakeCraftItem(PointerEventData evt)
    {
        if (curData == null)
            return;

        //재료 체크
        IsMake = new bool[3];
        UI_ItemSlot[] slots = Managers.UI.FindPopup<UI_Inventory>().slots;
        for (int i = 0; i < curData.NeedItem.Length; i++)
        {
            string NeedItem = curData.NeedItem[i];
            int NeedCost = curData.NeedCost[i];
            if (NeedItem == "")
            {
                IsMake[i] = true;
                continue;
            }
            for (int j = 0; j < slots.Length; j++)
            {
                if (slots[j].item == null)
                    continue;

                if (slots[j].item.name == NeedItem)
                {
                    if (NeedCost > slots[j].quantity)
                    {
                        Managers.UI.ShowPopupUI<UI_ItemWarning>();
                        return;
                    }
                    else
                    {
                        IsMake[i] = true;
                        break;
                    }

                }
            }
        }
        for (int i = 0; i < 3; i++)
        {
            if (!IsMake[i])
            {
                Managers.UI.ShowPopupUI<UI_ItemWarning>();
                return;
            }
        }

        Debug.Log("조합완료");
        //갯수 줄이기
        for (int i = 0; i < curData.NeedItem.Length; i++)
        {
            string NeedItem = curData.NeedItem[i];
            int NeedCost = curData.NeedCost[i];
            if (NeedItem == "")
            {
                IsMake[i] = true;
                continue;
            }
            for (int j = 0; j < slots.Length; j++)
            {
                if (slots[j].item == null)
                    continue;

                if (slots[j].item.name == NeedItem)
                {
                    slots[j].quantity -= NeedCost;
                    slots[j].Clear();
                    break;
                }
            }
        }
        ItemBase item = Managers.Resource.Load<ItemBase>(curData.SoPath);
        Managers.UI.FindPopup<UI_Inventory>().AddCraftItem(item);
        Managers.UI.ClosePopupUI();

    }

    void OnCloseButton(PointerEventData evt)
    {
        if (Cursor.lockState != CursorLockMode.Locked)
            Cursor.lockState = CursorLockMode.Locked;
        Managers.UI.ClosePopupUI();
    }
}
