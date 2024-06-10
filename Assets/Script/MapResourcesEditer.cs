using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class MapResourcesEditer : MonoBehaviour
{
    float Width = 50f;
    float Length = 50f;

    private GameObject Tree;
    private GameObject NPC;
    private GameObject Rock;

    public Transform[] point;

    private List<Vector3> spawnPos;
    private void Start()
    {
        spawnPos = new List<Vector3>();
        Tree = Managers.Resource.Load<GameObject>("Prefabs/Resource/Tree");
        NPC = Managers.Resource.Load<GameObject>("Prefabs/Resource/NPC");
        Rock = Managers.Resource.Load<GameObject>("Prefabs/Resource/Rock");
    }

    [ContextMenu("Asd")]
    private void Generate()
    {
        int idx = Random.Range(0, 4);
        Transform selectPoisition = point[idx];


        Vector3 Spawn = selectPoisition.position;
        Spawn += SpwanPoint(selectPoisition.GetComponent<GenerateFloat>().radius);
        foreach (Vector3 pos in spawnPos)
        {
            if(Vector3.Distance(pos,Spawn) <=   4f)
            {
                Generate();
                return;
            }
        }

        spawnPos.Add(Spawn);
        Managers.Resource.Instantiate(Tree).transform.position = Spawn;
    }
    private Vector3 SpwanPoint(float radius)
    {
        Vector3 getPoint = Random.onUnitSphere;
        getPoint.y = 0.0f;
        float r = Random.Range(0.0f, radius);
        return getPoint * r;
    }
}