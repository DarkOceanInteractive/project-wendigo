using System.Collections;
using UnityEngine;

namespace ProjectWendigo.PlayerJumpStates
{
    public class Jumping : AState<PlayerJumpStateContext>
    {
        public override void Enter()
        {
            // To jump at height h: v.y = sqrt(h * 2 * g) As Physics.gravity.y is already negative, v.y = sqrt(h * 2 * -g)
            this.context.VerticalSpeed = Mathf.Sqrt(this.context.JumpHeight * 2f * -Physics.gravity.y);
            this.context.StartCoroutine(this.WaitAndFall());
        }

        private IEnumerator WaitAndFall()
        {
            // Let one FixedUpdate pass before switching state to Falling,
            // otherwise with too high framerate, the CharacterController
            // might still be grounded on upcoming check (in Falling state).
            yield return new WaitForFixedUpdate();
            this.context.SetState(new Falling());
        }
    }
}
