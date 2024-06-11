using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class GameData
{
    public bool BGMon = true;
    public bool EffectSoundon = true;
    public int CharacterIdx;
    public GameData()
    {
        BGMon = true;
        EffectSoundon = true;
    }
}

public class GameManagerEx
{
    GameData _gameData = new GameData();
    public GameData SaveData { get { return _gameData; } set { _gameData = value; } }

    #region Option
    public bool BGMOn
    {
        get { return _gameData.BGMon; }
        set { _gameData.BGMon = value; }
    }

    public bool EffectSoundOn
    {
        get { return _gameData.EffectSoundon; }
        set { _gameData.EffectSoundon = value; }
    }
    #endregion

    public bool IsLoaded = false;

    public void Init()
    {
        _path = Application.persistentDataPath + "/SaveData.json";
        if (LoadGame())
            return;

        IsLoaded = true;

        SaveGame();
    }

    #region Save&Load
    string _path;

    public void SaveGame()
    {
        string jsonStr = JsonUtility.ToJson(Managers.Game.SaveData);
        File.WriteAllText(_path, jsonStr);
    }

    public bool LoadGame()
    {
        if (File.Exists(_path) == false)
            return false;

        string fileStr = File.ReadAllText(_path);
        GameData data = JsonUtility.FromJson<GameData>(fileStr);
        if (data != null)
            Managers.Game.SaveData = data;

        IsLoaded = true;
        return true;
    }
    #endregion
}
