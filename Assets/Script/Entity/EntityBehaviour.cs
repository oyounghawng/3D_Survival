using System;
using UnityEngine;

public class EntityBehaviour : MonoBehaviour, IMoveable
{
    protected Rigidbody body;

    [Header("EntityInfo")]
    protected string entityName;
    [Range(0f, 100f)] protected float attack;
    [Range(0f, 100f)] protected float hp;

    [Header("MoveState")]
    [SerializeField] [Range(0f, 5f)] protected float moveSpeed;
    [SerializeField] [Range(0f, 10f)] protected float runSpeed;
    protected float curSpeed;

    

    protected virtual void Awake()
    {
        body = GetComponent<Rigidbody>();
        curSpeed = moveSpeed;
    }

    public virtual void Move(Vector2 direction)
    {
        Vector3 move = transform.forward * direction.y + transform.right * direction.x;
        move *= curSpeed;
        move.y = body.velocity.y;
        body.velocity = move;
    }

    public virtual void Run(bool value)
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

}