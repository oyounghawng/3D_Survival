using System;
using System.Collections.Generic;

[Serializable]
public class LevelExpData
{
    public int Game_Lv;
    public int Game_Lv_Exp;
}

[Serializable]
public class LevelExpDataLoader : ILoader<int, LevelExpData>
{
    public List<LevelExpData> levelExps = new List<LevelExpData>();

    public Dictionary<int, LevelExpData> MakeDict()
    {
        Dictionary<int, LevelExpData> dic = new Dictionary<int, LevelExpData>();

        foreach (LevelExpData levelExp in levelExps)
            dic.Add(levelExp.Game_Lv, levelExp);

        return dic;
    }
}