using UnityEngine;

namespace ProjectWendigo
{
    public class LockMovementForward : MonoBehaviour
    {
        public void LockMovementDirection(GameObject target)
        {
            Singletons.Main.Camera.FocusOnTarget(target);
            Singletons.Main.Camera.LockCamera();
            Singletons.Main.Input.SetPlayerMovementFilter(movement => new Vector2(0, Mathf.Max(0, movement.y)));
        }

        public void UnlockMovementDirection()
        {
            Singletons.Main.Camera.UnlockCamera();
            Singletons.Main.Input.ResetPlayerMovementFilter();
        }
    }
}