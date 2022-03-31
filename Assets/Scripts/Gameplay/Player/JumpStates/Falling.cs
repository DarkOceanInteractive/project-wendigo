using UnityEngine;

namespace ProjectWendigo.PlayerJumpStates
{
    public class Falling : AState<PlayerJumpStateContext>
    {
        public override void Update()
        {
            if (this.context.IsGrounded)
            {
                this.context.SetState(new Idle());
            }
            else
            {
                // v(t).y = -g * t => deltaV(t).y = -g * deltaT
                // As Physics.gravity.y is already negative, deltaV(t).y = -(-g) * deltaT = g * deltaT
                this.context.VerticalSpeed += this.context.GravityMultiplier * Physics.gravity.y * Time.deltaTime;
            }
        }
    }
}
