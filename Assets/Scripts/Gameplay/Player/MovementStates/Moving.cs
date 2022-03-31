using UnityEngine;

namespace ProjectWendigo.PlayerMovementStates
{
    public class Moving : AState<PlayerMovementStateContext>
    {
        private static readonly float _speedMultiplier = 1f;

        public override void Enter()
        {
            this.context.SpeedMultiplier = Moving._speedMultiplier;
        }

        public override void Update()
        {
            if (Input.GetButtonDown("Crouch"))
                this.context.SetState(new Crouching());
            else if (Input.GetKey(KeyCode.LeftShift))
                this.context.SetState(new Sprinting());
            else if (Input.GetAxis("Horizontal") == 0f && Input.GetAxis("Vertical") == 0f)
                this.context.SetState(new Idle());
        }
    }
}
