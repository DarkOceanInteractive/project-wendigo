using UnityEngine;

namespace ProjectWendigo
{
    public class PlayerController : MonoBehaviour
    {
        private Transform _cameraTransform;
        [SerializeField] private CharacterController _characterController;
        private Vector3 _motion = Vector3.zero;

        protected void Awake()
        {
            this._cameraTransform = Camera.main.transform;
        }

        protected void Update()
        {
            if (this._motion != Vector3.zero)
            {
                this._motion = this.TransformToCameraView(this._motion);
                _ = this._characterController.Move(this._motion);
                this._motion = Vector3.zero;
            }
        }

        /// <summary>
        /// Move the character controller by `motion`.
        /// </summary>
        /// <param name="motion">Motion to apply</param>
        public void Move(Vector3 motion)
        {
            this._motion += motion;
        }

        /// <summary>
        /// Transform a direction vector along the camera view direction.
        /// </summary>
        /// <param name="direction">Direction vector to transform</param>
        /// <returns>Direction vector transformed along the camera view direction</returns>
        private Vector3 TransformToCameraView(Vector3 direction)
        {
            // Extract the camera forward direction along the x and z axes.
            Vector3 cameraForward = this._cameraTransform.forward;
            Vector3 horizontalCameraForward = new Vector3(cameraForward.x, 0f, cameraForward.z).normalized;

            // Extract the camera right direction along the x and z axes.
            Vector3 cameraRight = this._cameraTransform.right;
            Vector3 horizontalCameraRight = new Vector3(cameraRight.x, 0f, cameraRight.z).normalized;

            return horizontalCameraForward * direction.z
              + Vector3.up * direction.y
              + horizontalCameraRight * direction.x;
        }

    }
}
