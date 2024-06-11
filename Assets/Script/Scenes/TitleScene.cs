using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScene : BaseScene
{
    protected override void Init()
    {
        base.Init();

        SceneType = Define.SceneType.TitleScene;
        StartCoroutine(CoWaitLoad());
    }

    IEnumerator CoWaitLoad()
    {
        while (Managers.Data.Loaded() == false)
            yield return null;

        Managers.Sound.Play(Define.Sound.Bgm, "Bgm/TitleSceneBGM", 0.5f);

    }
    public override void Clear()
    {
    }
}