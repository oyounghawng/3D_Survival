using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class Resource : MonoBehaviour, IDamagable
{
    [SerializeField] private List<GameObject> dropPrefabs;
    [SerializeField] private float dropHeight = 2;
    [SerializeField] private float MaxHP;
    private float nowHP;

    private void Awake()
    {
        nowHP = MaxHP;
    }

    public void Damaged(float damage)
    {
        //Debug.Log("damaged");
        if(damage <= 0)
        {
            return;
        }

        nowHP -= damage;

        Instantiate(dropPrefabs[Random.Range(0, dropPrefabs.Count)], transform.position + Vector3.up * dropHeight, Quaternion.identity);

        if(isDie())
        {
            Destroy(gameObject);
        }


    }

    public bool isDie()
    {
        return nowHP <= 0;
    }
}
