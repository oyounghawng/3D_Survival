using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_BuildSlot : UI_Base
{
    //슬릇에 아이템 데이터 저장하기
    //지금은 임시로 대충 벽만 만드는걸로
    enum Images
    {
        Icon,
    }
    enum Buttons
    {
        BuildButton,
    }
    private void Start()
    {
        Init();
    }
    public override void Init()
    {
        Bind<Image>(typeof(Images));
        Bind<Button>(typeof(Buttons));

        Get<Button>((int)Buttons.BuildButton).gameObject.BindEvent(BuildItem);
    }
    public void BuildItem(PointerEventData evt)
    {
        //데이터를 가져와서 아이템 프리팹 경로를 받아온다.
        Managers.Resource.Instantiate("Build/WallPreview");
    }
}
