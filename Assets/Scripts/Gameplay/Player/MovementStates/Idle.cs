using UnityEngine;

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
            if (Input.GetButtonDown("Crouch"))
                this.context.SetState(new Crouching());
            else if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
                this.context.SetState(new Moving());
        }
    }
}
