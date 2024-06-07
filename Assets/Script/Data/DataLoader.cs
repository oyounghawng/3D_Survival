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

[Serializable]
public class CraftItemData
{
    public int Craft_Id;
    public string ItemName;
    public string ItemInfo;
    public string[] NeedItem;
}

[Serializable]
public class CraftItemDataLoader : ILoader<int, CraftItemData>
{
    public List<CraftItemData> craftItems = new List<CraftItemData>();

    public Dictionary<int, CraftItemData> MakeDict()
    {
        Dictionary<int, CraftItemData> dic = new Dictionary<int, CraftItemData>();

        foreach (CraftItemData craftItem in craftItems)
            dic.Add(craftItem.Craft_Id, craftItem);

        return dic;
    }
}