using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum AIState
{
    IDLE,
    WANDERING,
    ATTACKING
}

public class NPC : EntityBehaviour
{
    [Header("Stats")]
    public ItemBase[] dropOnDeath;

    [Header("AI")]
    private NavMeshAgent agent;
    public float detectDistance;
    private AIState aiState;

    [Header("Wandering")]
    public float minWanderDistance;
    public float maxWanderDistance;
    public float minWanderWaitTime;
    public float maxWanderWaitTime;

    [Header("Combat")]
    public int damage;
    public float attackRate;
    private float lastAttackTime;
    public float attackDistance;

    private float playerDistance;

    public float fieldOfView = 120f;

    private Animator animator;
    private SkinnedMeshRenderer[] meshRenderers;

    protected override void Awake()
    {
        base.Awake();
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        meshRenderers = GetComponentsInChildren<SkinnedMeshRenderer>();
    }

    private void Start()
    {
        SetState(AIState.WANDERING);
    }

    private void Update()
    {
        playerDistance = Vector3.Distance(transform.position, Managers.Object.Player.transform.position);

        animator.SetBool("Moving", aiState != AIState.IDLE);

        switch(aiState)
        {
            case AIState.IDLE:
            case AIState.WANDERING:
                PassiveUpdate();
                break;
            case AIState.ATTACKING:
                AttackingUpdate();
                break;
        }
    }

    public void SetState(AIState state)
    {
        aiState = state;

        switch(aiState)
        {
            case AIState.IDLE:
                agent.speed = moveSpeed;
                agent.isStopped = true;
                break;
            case AIState.WANDERING:
                agent.speed = moveSpeed;
                agent.isStopped = false;
                break;
            case AIState.ATTACKING:
                agent.speed = runSpeed;
                agent.isStopped = false;
                break;
        }

        animator.speed = agent.speed / moveSpeed;
    }

    private void PassiveUpdate()
    {
        if(aiState == AIState.WANDERING && agent.remainingDistance < 0.1f)
        {
            SetState(AIState.IDLE);
            Invoke("WanderToNewLocation", Random.Range(minWanderWaitTime, maxWanderWaitTime));
        }

        if(playerDistance < detectDistance)
        {
            SetState(AIState.ATTACKING);
        }
    }

    private void WanderToNewLocation()
    {
        if (aiState != AIState.IDLE) return;

        SetState(AIState.WANDERING);
        agent.SetDestination(GetWanderLocation());
        
    }

    // do-while로 고쳐보기
    private Vector3 GetWanderLocation()
    {
        NavMeshHit hit;

        NavMesh.SamplePosition(
            transform.position + (Random.onUnitSphere * Random.Range(minWanderDistance,maxWanderDistance)),
            out hit, 
            maxWanderDistance, 
            NavMesh.AllAreas
            );
        int i = 0;

        while(Vector3.Distance(transform.position, hit.position) < detectDistance)
        {
            NavMesh.SamplePosition(
            transform.position + (Random.onUnitSphere * Random.Range(minWanderDistance, maxWanderDistance)),
            out hit,
            maxWanderDistance,
            NavMesh.AllAreas
            );

            i++;

            if (i == 30) break;
        }

        return hit.position;

    }

    private void AttackingUpdate()
    {
        if (playerDistance < attackDistance && isPlayerInFieldOfView())
        {
            agent.isStopped = true;
            if (Time.time - lastAttackTime > attackRate)
            {
                lastAttackTime = Time.time;
                Managers.Object.Player.GetComponent<IDamagable>().Damaged(damage);
                animator.speed = 1;
                animator.SetTrigger("Attack");
            }
        }
        else
        {
            if (playerDistance < detectDistance)
            {
                agent.isStopped = false;
                NavMeshPath path = new NavMeshPath();
                if (agent.CalculatePath(Managers.Object.Player.transform.position, path))
                {
                    agent.SetDestination(Managers.Object.Player.transform.position);
                }
                else
                {
                    agent.SetDestination(transform.position);
                    agent.isStopped = true;
                    SetState(AIState.WANDERING);
                }
            }
            else
            {
                agent.SetDestination(transform.position);
                agent.isStopped = true;
                SetState(AIState.WANDERING);
            }
        }
    }

    private bool isPlayerInFieldOfView()
    {
        Vector3 directionToPlayer = Managers.Object.Player.transform.position - transform.position;
        float angle = Vector3.Angle(transform.forward, directionToPlayer);
        return angle < fieldOfView * 0.5f;
    }

    public override void Damaged(float damage)
    {
        base.Damaged(damage);

        if(isDie())
        {
            Die();
        }

        StartCoroutine(DamageFlash());
    }

    private void Die()
    {
        for (int i = 0; i < dropOnDeath.Length; i++)
        {
            Instantiate(dropOnDeath[i].prefab, transform.position + Vector3.up * 2, Quaternion.identity);
        }

        Destroy(gameObject);
    }

    private IEnumerator DamageFlash()
    {
        for(int i=0; i < meshRenderers.Length; i++)
        {
            meshRenderers[i].material.color = new Color(1.0f, 0.6f, 0.6f);
        }

        yield return new WaitForSeconds(0.1f);

        for(int i=0; i<meshRenderers.Length; i++)
        {
            meshRenderers[i].material.color = Color.white;
        }
    }
}
