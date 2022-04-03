namespace ProjectWendigo.PlayerMovementStates
{
    public class Sprinting : AState<PlayerMovementStateContext>
    {
        public override void Enter()
        {
            this.context.SpeedMultiplier = this.context.SprintingSpeedMultiplier;
        }

        public override void Update()
        {
            if (Singletons.Main.Input.PlayerStartedCrouching)
                this.context.SetState(new Crouching());
            else if (Singletons.Main.Input.PlayerStoppedSprinting)
                this.context.SetState(new Moving());
            else if (!Singletons.Main.Input.PlayerIsMoving)
                this.context.SetState(new Idle());
        }
    }
}
