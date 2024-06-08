using System;
using UnityEngine;

public class EntityBehaviour : MonoBehaviour, IDamagable
{
    protected Rigidbody body;

    [Header("EntityInfo")]
    protected string entityName;
    [SerializeField] protected float attack;
    [SerializeField] protected float MaxHP;
    protected float curHP;

    [Header("MoveState")]
    [SerializeField] [Range(0f, 5f)] protected float moveSpeed;
    [SerializeField] [Range(0f, 10f)] protected float runSpeed;
    protected float curSpeed;

    

    protected virtual void Awake()
    {
        body = GetComponent<Rigidbody>();
        curSpeed = moveSpeed;
    }

    protected virtual void Move(Vector2 direction)
    {
        Vector3 move = transform.forward * direction.y + transform.right * direction.x;
        move *= curSpeed;
        move.y = body.velocity.y;
        body.velocity = move;
    }

    protected virtual void Run(bool value)
    {
        if(value)
        {
            curSpeed = runSpeed;
        }
        else
        {
            curSpeed = moveSpeed;
        }
    }

    public bool Damaged(float damage)
    {
        curHP -= damage;

        if (damage <= 0)
            return false;

        return true;
    }

    public bool isDie()
    {
        return curHP <= 0;
    }


}