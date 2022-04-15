using UnityEngine;

namespace ProjectWendigo
{
    [System.Serializable]
    public struct RotationAxes
    {
        public bool x;
        public bool y;
        public bool z;

        public RotationAxes(bool x, bool y, bool z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
    }

    public class FollowCamera : MonoBehaviour
    {
        public Camera PlayerCamera;
        public RotationAxes Axes = new RotationAxes(false, false, false);
        private Vector3 _target;

        void Start()
        {
            if (!this.PlayerCamera)
            {
                this.PlayerCamera = Camera.main;
            }
        }

        void Update()
        {
            this._target = this.transform.rotation.eulerAngles;
            if (this.Axes.x)
            {
                this._target.x = this.PlayerCamera.transform.rotation.eulerAngles.x;
            }
            if (this.Axes.y)
            {
                this._target.y = this.PlayerCamera.transform.rotation.eulerAngles.y;
            }
            if (this.Axes.z)
            {
                this._target.z = this.PlayerCamera.transform.rotation.eulerAngles.z;
            }
            this.transform.rotation = Quaternion.Euler(this._target);
        }
    }
}
