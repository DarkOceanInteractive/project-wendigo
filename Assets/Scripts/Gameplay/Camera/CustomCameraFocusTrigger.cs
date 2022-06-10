using UnityEngine;

namespace ProjectWendigo
{
    public class CustomCameraFocusTrigger : MonoBehaviour
    {
        public float MinBlendTime = 1f;
        public float MaxBlendTime = 2f;

        public void FocusCameraOnTarget(GameObject target)
        {
            Debug.Assert(this.MaxBlendTime >= this.MinBlendTime);
            Singletons.Main.Camera.FocusOnTarget(target, this.MinBlendTime, this.MaxBlendTime);
        }
    }
}