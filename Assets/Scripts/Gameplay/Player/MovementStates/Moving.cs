namespace ProjectWendigo.PlayerMovementStates
{
    public class Moving : AState<PlayerMovementStateContext>
    {
        private static readonly float _speedMultiplier = 1f;

        public override void Enter()
        {
            this.context.SpeedMultiplier = _speedMultiplier;
        }

        public override void Update()
        {
            if (Singletons.Main.Input.PlayerStartedCrouching)
                this.context.SetState(new Crouching());
            else if (Singletons.Main.Input.PlayerStartedSprinting)
                this.context.SetState(new Sprinting());
            else if (!Singletons.Main.Input.PlayerIsMoving)
                this.context.SetState(new Idle());
        }
    }
}
