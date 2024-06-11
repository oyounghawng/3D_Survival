using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour, IDamagable
{
    [SerializeField] private List<GameObject> dropPrefabs;
    [SerializeField] private float dropHeight = 2;
    [SerializeField] private float MaxHP;
    private float nowHP;

    public Define.Resources resourceType;
    private void Awake()
    {
        nowHP = MaxHP;
    }

    public void Damaged(float damage)
    {
        //Debug.Log("damaged");
        if (damage <= 0)
        {
            return;
        }

        nowHP -= damage;

        Instantiate(dropPrefabs[Random.Range(0, dropPrefabs.Count)], transform.position + Vector3.up * dropHeight, Quaternion.identity);

        if (isDie())
        {
            MapResourcesEditer.instace.spawnPos.Remove(this.gameObject);
            MapResourcesEditer.instace.ReGnerate(resourceType);
            Destroy(gameObject);
        }


    }

    public bool isDie()
    {
        return nowHP <= 0;
    }
}
