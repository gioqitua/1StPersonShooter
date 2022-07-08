using System;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStateMachine : StateMachine, IDamagable
{
    public int health = 15;
    internal float damagePerHit = 15f;
    public ParticleSystem bloodEffect;
    public NavMeshAgent navmeshAgent;
    public float chasingRadius;
    public float lookRadius;
    public float attackRange;
    public Animator anim;
    public Transform playerPos;
    public event Action attackComplete;
    private void Start()
    {
        playerPos = GameObject.Find("Player").transform;
        navmeshAgent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        SwitchState(new RoamingState(this));
    }
    public void TakeDamage(int value)
    {
        health -= value;

        if (health <= 0)
        {
            Die();
        }
    }
    public float CalculateDistance()
    {
        return Vector3.Distance(this.transform.position, playerPos.position);
    }
    private void Die()
    {
        Destroy(this.gameObject);
    }
    #region animation callbacks
    public void AttackComplete()
    {
        attackComplete?.Invoke();
    }
    #endregion
}
