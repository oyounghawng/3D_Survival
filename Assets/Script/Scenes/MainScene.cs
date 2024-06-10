using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScene : BaseScene
{
    protected override void Init()
    {
        base.Init();

        SceneType = Define.SceneType.MainScene;
        StartCoroutine(CoWaitLoad());
    }

    IEnumerator CoWaitLoad()
    {
        while (Managers.Data.Loaded() == false)
            yield return null;

        Managers.UI.ShowSceneUI<UI_HUD>();
        Managers.Object.SpawnPlayer();
    }
    public override void Clear()
    {
    }
}
