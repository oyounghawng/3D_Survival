using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Build : UI_Popup
{
    enum GameObjects
    {
        BuildList,
    }
    void Start()
    {
        Init();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Managers.UI.ClosePopupUI();
        }
    }
    public override void Init()
    {
        base.Init();
        Bind<GameObject>(typeof(GameObjects));

        SetBuildList();
        if (Cursor.lockState == CursorLockMode.Locked)
            Cursor.lockState = CursorLockMode.None;
    }

    private void SetBuildList()
    {
        GameObject go = Get<GameObject>((int)GameObjects.BuildList);

        foreach (Transform child in go.transform)
            Managers.Resource.Destroy(child.gameObject);

        for (int i = 0; i < Managers.Data.BuildItems.Count; i++)
        {
            BuildItemData bData;
            Managers.Data.BuildItems.TryGetValue(i, out bData);
            UI_BuildSlot buildSlot = Managers.UI.MakeSubItem<UI_BuildSlot>(go.transform);
            buildSlot.SetBuildData(bData);
        }
    }
}
