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
    }
    public override void Clear()
    {
    }
}
