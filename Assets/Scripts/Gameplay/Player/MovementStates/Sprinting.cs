using UnityEngine;

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
            if (Input.GetButtonDown("Crouch"))
                context.SetState(new Crouching());
            else if (!Input.GetKey(KeyCode.LeftShift))
                context.SetState(new Moving());
            else if (Input.GetAxis("Horizontal") == 0f && Input.GetAxis("Vertical") == 0f)
                context.SetState(new Idle());
        }
    }
}
