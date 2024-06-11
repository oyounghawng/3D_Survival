using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_BuildSlot : UI_Base
{
    //슬릇에 아이템 데이터 저장하기
    //지금은 임시로 대충 벽만 만드는걸로
    BuildItemData data;
    enum Images
    {
        Icon,
    }
    enum Buttons
    {
        BuildButton,
    }
    enum Texts
    {
        BuildItem,
        NeedItem
    }

    private void Start()
    {
        Init();
    }
    public override void Init()
    {
        Bind<Image>(typeof(Images));
        Bind<Button>(typeof(Buttons));
        Bind<TextMeshProUGUI>(typeof(Texts));

        GetButton((int)Buttons.BuildButton).gameObject.BindEvent(BuildItem);
        GetText((int)Texts.BuildItem).text = data.ItemName;
        GetText((int)Texts.NeedItem).text = $"{data.NeedItem} {data.NeedCost}개";
    }
    public void SetBuildData(BuildItemData _data)
    {
        data = _data;
    }
    private void BuildItem(PointerEventData evt)
    {
        if (data == null)
            return;
        /*
        UI_ItemSlot[] slots = Managers.UI.FindPopup<UI_Inventory>().slots;
        string NeedItem = data.NeedItem;
        int NeedCost = data.NeedCost;
        bool isBuild = false;
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
                    isBuild = true;
                    break;
                }

            }
        }
        if (!isBuild)
        {
            Managers.UI.ShowPopupUI<UI_ItemWarning>();
            return;
        }
        Debug.Log("건축 완료");
        //갯수 줄이기
        for (int j = 0; j < slots.Length; j++)
        {
            if (slots[j].item == null)
                continue;

            if (slots[j].item.name == NeedItem)
            {
                slots[j].quantity -= NeedCost;
                if (slots[j].quantity <= 0)
                    slots[j].Clear();

                break;
            }
        }
        */
        //건축
        GameObject go = Managers.Resource.Instantiate(data.PrefabPath);
        Color color = go.GetComponent<MeshRenderer>().material.color;
        go.GetComponent<MeshRenderer>().material.color = new Color(color.r, color.g, color.b, 0.5f);
        go.transform.position = Managers.Object.Player.transform.position;

        if (Cursor.lockState == CursorLockMode.None)
            Cursor.lockState = CursorLockMode.Locked;
        Managers.UI.ClosePopupUI();
        Managers.UI.ClosePopupUI();
    }
}
