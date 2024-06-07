using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILoader<Key, Value>
{
    Dictionary<Key, Value> MakeDict();
}
public class DataManager
{

    //미니게임 데이터
    //public Dictionary<int, LevelExpData> LevelExps { get; private set; } = new Dictionary<int, LevelExpData>();
    public Dictionary<int, CraftItemData> CraftItems { get; private set; } = new Dictionary<int, CraftItemData>();
    public void Init()
    {
        //LevelExps = LoadJson<LevelExpDataLoader, int, LevelExpData>("LevelExpData").MakeDict();
        CraftItems = LoadJson<CraftItemDataLoader, int, CraftItemData>("CraftItemData").MakeDict();
    }

    public bool Loaded()
    {
        return true;
    }

    Loader LoadJson<Loader, Key, Value>(string path) where Loader : ILoader<Key, Value>
    {
        TextAsset textAsset = Managers.Resource.Load<TextAsset>($"Data/Json/{path}");
        return JsonUtility.FromJson<Loader>(textAsset.text);
    }
}