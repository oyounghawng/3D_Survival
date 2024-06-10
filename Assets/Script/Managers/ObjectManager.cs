using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager
{
    private GameObject player;
    public GameObject Player { get => player; }

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
