namespace ProjectWendigo.PlayerJumpStates
{
    public class Idle : AState<PlayerJumpStateContext>
    {
        public override void Enter()
        {
            // Reset vertical speed when hitting the ground
            this.context.VerticalSpeed = -1f;
        }

        public override void Update()
        {
            if (this.context.IsGrounded)
            {
                if (Singletons.Main.Input.PlayerJumped)
                    this.context.SetState(new Jumping());
            }
            else
            {
                this.context.SetState(new Falling());
            }
        }
    }
}
