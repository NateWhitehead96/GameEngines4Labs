using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(StateMachine))]

public class ZombieComponent : MonoBehaviour
{
    public NavMeshAgent ZombieNavMesh { get; private set; }

    public Animator ZombieAnimator{get; private set; }

    public StateMachine StateMachine { get; private set; }

    public GameObject FollowTarget;

    public bool Debug;

    public float ZombieDamage => damage;
    [SerializeField] float damage;
    private void Awake()
    {
        ZombieNavMesh = GetComponent<NavMeshAgent>();
        ZombieAnimator = GetComponent<Animator>();
        StateMachine = GetComponent<StateMachine>();
         
    }
    // Start is called before the first frame update
    void Start()
    {
        if(Debug)
        {
            Initialize(FollowTarget);
        }
    }

    public void Initialize(GameObject followTarget)
    {
        FollowTarget = followTarget;
        ZombieIdleState idleState = new ZombieIdleState(this, StateMachine);
        StateMachine.AddState(ZombieStateType.IDLE, idleState);

        ZombieFollowState followState = new ZombieFollowState(FollowTarget, this, StateMachine);
        StateMachine.AddState(ZombieStateType.FOLLOW, followState);

        ZombieAttackState attackState = new ZombieAttackState(FollowTarget, this, StateMachine);
        StateMachine.AddState(ZombieStateType.ATTACK, attackState);

        ZombieDeadState deadState = new ZombieDeadState(this, StateMachine);
        StateMachine.AddState(ZombieStateType.DEAD, deadState);

        StateMachine.Initialize(ZombieStateType.FOLLOW);
    }
}
