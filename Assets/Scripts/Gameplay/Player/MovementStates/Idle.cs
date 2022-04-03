namespace ProjectWendigo.PlayerMovementStates
{
    public class Idle : AState<PlayerMovementStateContext>
    {
        public override void Enter()
        {
            this.context.SpeedMultiplier = 0f;
        }

        public override void Update()
        {
            if (Singletons.Main.Input.PlayerStartedCrouching)
                this.context.SetState(new Crouching());
            else if (Singletons.Main.Input.PlayerIsMoving)
                this.context.SetState(new Moving());
        }
    }
}
