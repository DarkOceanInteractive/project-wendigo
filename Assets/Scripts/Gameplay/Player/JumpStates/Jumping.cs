using UnityEngine;

namespace ProjectWendigo.PlayerJumpStates
{
    public class Jumping : AState<PlayerJumpStateContext>
    {
        public override void Enter()
        {
            // To jump at height h: v.y = sqrt(h * 2 * g) As Physics.gravity.y is already negative, v.y = sqrt(h * 2 * -g)
            this.context.VerticalSpeed = Mathf.Sqrt(this.context.JumpHeight * 2f * -Physics.gravity.y);
        }

        public override void Update()
        {
            this.context.SetState(new Falling());
        }
    }
}
