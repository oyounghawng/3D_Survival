using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager
{
    private Player player;

    public Player Player { get => player; set { player = value; } }

    public ObjectManager()
    {
        Init();
    }
    public void Init()
    {
        
    }

    public void SpawnPlayer()
    {
        GameObject go = Managers.Resource.Instantiate("Player");
        player = go;
        Managers.Resource.Instantiate("CameraContains");
    }
}
