using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_BuildSlot : UI_Base
{
    //������ ������ ������ �����ϱ�
    //������ �ӽ÷� ���� ���� ����°ɷ�
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
        //�����͸� �����ͼ� ������ ������ ��θ� �޾ƿ´�.
        Managers.Resource.Instantiate("Build/WallPreview");
    }
}
