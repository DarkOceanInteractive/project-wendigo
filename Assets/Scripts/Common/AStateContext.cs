using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// State context abstract base class. A state context is responsible for
/// holding a state machine's current state and if necessary the public
/// variables needed by the different states. AStateContext provides a default
/// implementation for the different MonoBehaviour hooks, as well as a
/// SetState which can be called to change the current state.
/// </summary>
public abstract class AStateContext : MonoBehaviour
{
    public UnityEvent<AStateContext, IState> OnChangeState;

    private IState _state;

    public IState State
    {
        get { return this._state; }
    }

    /// <summary>
    /// Returns true if the current state is the given state.
    /// </summary>
    public bool IsInState<StateT>()
      where StateT : IState
    {
        return this.State.GetType() == typeof(StateT);
    }

    /// <summary>
    /// Change the current state.
    /// </summary>
    public virtual void SetState(IState value)
    {
        if (this._state != null)
            this._state.Exit();
        this._state = value;
        value.SetContext(this);
        this._state.Enter();
        this.OnChangeState?.Invoke(this, this._state);
    }

    protected virtual void FixedUpdate()
    {
        this.State.FixedUpdate();
    }

    protected virtual void LateUpdate()
    {
        this.State.LateUpdate();
    }

    protected virtual void Update()
    {
        this.State.Update();
    }
}
