using UnityEngine;
using Cinemachine;

namespace ProjectWendigo
{
    public class CameraFocus : MonoBehaviour
    {
        public float DefaultBlendTime;

        private float CalculateLookAtAngle(GameObject target)
        {
            Vector3 lookAtDirection = target.transform.position - Singletons.Main.Camera.PlayerCamera.transform.position;
            return Vector3.Angle(lookAtDirection, Camera.main.transform.forward);
        }

        private Quaternion CalculateLookAtRotation(GameObject target)
        {
            Vector3 lookAtDirection = target.transform.position - Singletons.Main.Camera.PlayerCamera.transform.position;
            Vector3 cross = Vector3.Cross(Vector3.forward, lookAtDirection.normalized);
            float angle = Mathf.Acos(
                Vector3.Dot(Vector3.forward, lookAtDirection.normalized)
            ) * Mathf.Rad2Deg;
            return Quaternion.AngleAxis(angle, cross.normalized);
        }

        private float CalculateBlendTime(GameObject target, float? minBlendTime = null, float? maxBlendTime = null)
        {
            if (!minBlendTime.HasValue)
                minBlendTime = 0f;
            if (!maxBlendTime.HasValue)
                maxBlendTime = this.DefaultBlendTime;
            float angle = this.CalculateLookAtAngle(target);
            return minBlendTime.Value + (maxBlendTime.Value - minBlendTime.Value) * angle / 180f;
        }

        public void FocusCameraOnTarget(GameObject target, float? minBlendTime = null, float? maxBlendTime = null)
        {
            Singletons.Main.Camera.PlayerFocusCamera.LookAt = target.transform;
            float time = this.CalculateBlendTime(target, minBlendTime, maxBlendTime);
            Singletons.Main.Camera.BlendToCamera(Singletons.Main.Camera.PlayerFocusCamera, time);
            this.Invoke(nameof(this.UnfocusTarget), time, target);
        }

        public void UnfocusTarget(GameObject target)
        {
            // Make player camera look at the target after the camera blending
            Singletons.Main.Camera.PlayerCamera.ForceCameraPosition(Singletons.Main.Camera.PlayerCamera.transform.position, this.CalculateLookAtRotation(target));
            Singletons.Main.Camera.BlendToCamera(Singletons.Main.Camera.PlayerCamera, 0);
        }
    }
}