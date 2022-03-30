/// <summary>
/// State interface.
/// </summary>
public interface IState
{
    /// <summary>
    /// Method called when the game object enters this state.
    /// </summary>
    public void Enter();

    /// <summary>
    /// Method called when the game object exits this state.
    /// </summary>
    public void Exit();

    /// <summary>
    /// Method called when the game object MonoBehaviour's FixedUpdate method
    /// is called.
    /// </summary>
    public void FixedUpdate();

    /// <summary>
    /// Method called when the game object MonoBehaviour's LateUpdate method
    /// is called.
    /// </summary>
    public void LateUpdate();

    /// <summary>
    /// Method called when the game object MonoBehaviour's Update method is
    /// called.
    /// </summary>
    public void Update();

    /// <summary>
    /// Set the state context. This method should only be called by the state
    /// context itself.
    /// </summary>
    /// <param name="context">New state context</param>
    public void SetContext(AStateContext context);
}
