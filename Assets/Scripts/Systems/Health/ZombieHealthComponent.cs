using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHealthComponent : HealthComponant
{
    private StateMachine ZombieStateMachine;

    private void Awake()
    {
        ZombieStateMachine = GetComponent<StateMachine>();
    }


    public override void Destroy()
    {
        //if(ZombieStateMachine)
        //base.Destroy();
        ZombieStateMachine.ChangeState(ZombieStateType.DEAD);
    }
}
