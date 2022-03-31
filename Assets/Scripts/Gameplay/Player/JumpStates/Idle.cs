using UnityEngine;

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
                if (Input.GetButtonDown("Jump"))
                    this.context.SetState(new Jumping());
            }
            else
            {
                this.context.SetState(new Falling());
            }
        }
    }
}
