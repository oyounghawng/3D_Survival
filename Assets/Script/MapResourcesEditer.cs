using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapResourcesEditer : MonoBehaviour
{
    public static MapResourcesEditer instace;
    float Width = 50f;
    float Length = 50f;

    private GameObject Tree;
    private GameObject NPC;
    private GameObject Rock;

    public Transform[] point;
    public List<GameObject> spawnPos;
    private void Awake()
    {
        instace = this;
    }
    private void Start()
    {
        spawnPos = new List<GameObject>();
        Tree = Managers.Resource.Load<GameObject>("Prefabs/Resource/Tree");
        NPC = Managers.Resource.Load<GameObject>("Prefabs/Resource/NPC");
        Rock = Managers.Resource.Load<GameObject>("Prefabs/Resource/Rock");

        for (int i = 0; i < 5; i++)
        {
            Generate(Define.Resources.Tree);
            Generate(Define.Resources.Rock);
        }
        for (int i = 0; i < 2; i++)
        {
            Generate(Define.Resources.NPC);
        }
    }
    public void ReGnerate(Define.Resources resources)
    {
        StartCoroutine(WaitGenerate(resources));
    }

    IEnumerator WaitGenerate(Define.Resources resources)
    {
        yield return new WaitForSeconds(20f);
        Generate(resources);
    }
    public void Generate(Define.Resources resources)
    {
        int idx = Random.Range(0, 4);
        Transform selectPoisition = point[idx];
        Vector3 Spawn = selectPoisition.position;
        Spawn += SpwanPoint(selectPoisition.GetComponent<GenerateFloat>().radius);
        foreach (GameObject pos in spawnPos)
        {
            if (Vector3.Distance(pos.transform.position, Spawn) <= 4f)
            {
                Generate(resources);
                return;
            }
        }

        GameObject go = null;
        switch (resources)
        {
            case Define.Resources.Tree:
                go = Tree;
                break;
            case Define.Resources.Rock:
                go = Rock;
                break;
            case Define.Resources.NPC:
                go = NPC;
                break;
        }
        GameObject go1 = Managers.Resource.Instantiate(go);
        go1.transform.position = Spawn;
        spawnPos.Add(go1);
    }
    private Vector3 SpwanPoint(float radius)
    {
        Vector3 getPoint = Random.onUnitSphere;
        getPoint.y = 0.0f;
        float r = Random.Range(0.0f, radius);
        return getPoint * r;
    }
}