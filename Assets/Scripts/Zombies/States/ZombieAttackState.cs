using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAttackState : ZombieStates
{
    private readonly GameObject FollowTarget;
    private float AttackRange = 1.5f;

    private IDamagable damagableObject;
    public ZombieAttackState(GameObject followTarget,ZombieComponent zombie, StateMachine stateMachine) : base(zombie, stateMachine)
    {
        FollowTarget = followTarget;
        UpdateInterval = 2f;

        damagableObject = followTarget.GetComponent<IDamagable>();
    }
    // Start is called before the first frame update
    public override void Start()
    {
        OwnerZombie.ZombieNavMesh.isStopped = true;
        OwnerZombie.ZombieNavMesh.ResetPath();
        OwnerZombie.ZombieAnimator.SetFloat("Movement Z", 0.0f);
        OwnerZombie.ZombieAnimator.SetBool("isAttacking", true);


    }
    public override void IntervalUpdate()
    {
        base.IntervalUpdate();
        damagableObject?.TakeDamage(OwnerZombie.ZombieDamage);
        // add damage to object
    }

    // Update is called once per frame
    public override void Update()
    {
        OwnerZombie.transform.LookAt(FollowTarget.transform.position, Vector3.up);

        float distanceBetween = Vector3.Distance(OwnerZombie.transform.position, FollowTarget.transform.position);
        if(distanceBetween < AttackRange)
        {
            stateMachine.ChangeState(ZombieStateType.FOLLOW);
        }
        // add zombie health < 0 die
    }

  

    public override void Exit()
    {
        base.Exit();
        OwnerZombie.ZombieAnimator.SetBool("isAttacking", false);
    }
}
