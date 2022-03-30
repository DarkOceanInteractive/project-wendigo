/// <summary>
/// State abstract base class. It provides a simple way to implement new states
/// by giving a default implementation for all methods of the IState interface.
/// Only used methods should be overriden by child classes.
/// </summary>
public abstract class AState<ContextT> : IState
  where ContextT : AStateContext
{
    protected ContextT context;

    public virtual void Enter()
    {
    }

    public virtual void Exit()
    {
    }

    public virtual void FixedUpdate()
    {
    }

    public virtual void LateUpdate()
    {
    }

    public virtual void Update()
    {
    }

    public void SetContext(AStateContext context)
    {
        this.context = context as ContextT;
    }
}

/// <summary>
/// Default AState implementation, inheriting  from AStateContext.
/// </summary>
public abstract class AState : AState<AStateContext>
{
}
