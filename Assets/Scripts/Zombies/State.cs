public class State
{
    protected StateMachine stateMachine;
    public float UpdateInterval { get; protected set; } = 1;

    protected State(StateMachine state)
    {
        stateMachine = state;
    }

    public virtual void Start()
    {

    }

    public virtual void IntervalUpdate()
    {

    }

    public virtual void Update()
    {

    }

    public virtual void FixedUpdate()
    {

    }
    public virtual void Exit()
    {

    }
}
