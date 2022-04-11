using UnityEngine;
using Cinemachine;

namespace ProjectWendigo
{
    public class HeadBobbing : CinemachineExtension
    {
        [SerializeField]
        private Vector2Spline _spline = Vector2Spline.Linear(Vector2.zero, Vector2.zero);
        [SerializeField] private PlayerMovementStateContext _playerMovementStateContext;
        [SerializeField] private float _bobbingSpeed = 0.1f;
        [SerializeField] private float _bobbingAmount = 0.1f;
        private Vector3 _cameraOffset = Vector3.zero;
        private float _t = 0f;

        protected void OnDrawGizmos()
        {
            this._spline.DrawGizmos(this.transform.position + Vector3.up - Vector3.right * 0.5f);
        }

        protected override void PostPipelineStageCallback(
            CinemachineVirtualCameraBase vcam,
            CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
        {
            if (stage != CinemachineCore.Stage.Body)
                return;
            if (Singletons.Main == null)
                return;
            float bobbingSpeed = this._bobbingSpeed * this._playerMovementStateContext.SmoothSpeedMultiplier;
            if (Singletons.Main.Input.PlayerIsMoving)
            {
                this._t = (this._t + bobbingSpeed * Time.deltaTime) % 1f;
                this._cameraOffset = new Vector3(
                    0f,
                    this._spline.Interpolate(0f, this._bobbingAmount, this._t),
                    0f);
            }
            else
            {
                this._t = 0f;
                this._cameraOffset = new Vector3(
                     0f,
                     Mathf.Lerp(this._cameraOffset.y, 0f, Time.deltaTime * bobbingSpeed),
                     0f);
            }
            state.PositionCorrection += this._cameraOffset;
        }
    }
}
