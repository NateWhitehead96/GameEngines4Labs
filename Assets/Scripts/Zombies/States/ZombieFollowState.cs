using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieFollowState : ZombieStates
{
    private readonly GameObject FollowTarget;
    private const float StopDistance = 1f;
    public ZombieFollowState(GameObject followTarget, ZombieComponent zombie, StateMachine stateMachine) : base(zombie,stateMachine)
    {
        FollowTarget = followTarget;
        UpdateInterval = 2f;
    }
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        OwnerZombie.ZombieNavMesh.SetDestination(FollowTarget.transform.position);
    }

    public override void IntervalUpdate()
    {
        base.IntervalUpdate();
        OwnerZombie.ZombieNavMesh.SetDestination(FollowTarget.transform.position);
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        OwnerZombie.ZombieAnimator.SetFloat("Movement Z", OwnerZombie.ZombieNavMesh.velocity.normalized.z);
        if(Vector3.Distance(OwnerZombie.transform.position, FollowTarget.transform.position) < StopDistance)
        {
            stateMachine.ChangeState(ZombieStateType.ATTACK);
        }
        // to add health
    }
}
