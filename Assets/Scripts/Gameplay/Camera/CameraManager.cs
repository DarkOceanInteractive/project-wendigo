using UnityEngine;
using Cinemachine;

namespace ProjectWendigo
{
    public class CameraManager : MonoBehaviour
    {
        public CinemachineBrain Brain;
        public CinemachineVirtualCamera PlayerCamera;
        public CinemachineVirtualCamera PlayerFocusCamera;
        private CameraFocus _focus;

        private void Awake()
        {
            Debug.Assert(this.Brain != null || Camera.main.TryGetComponent(out this.Brain));
            Debug.Assert(this._focus != null || this.TryGetComponent(out this._focus));
        }

        public void LockCamera()
        {
            this.PlayerCamera.gameObject.GetComponent<CinemachineInputProvider>().enabled = false;
        }

        public void UnlockCamera()
        {
            this.PlayerCamera.gameObject.GetComponent<CinemachineInputProvider>().enabled = true;
        }

        private void SetBlendTime(float time)
        {
            Singletons.Main.Camera.Brain.m_DefaultBlend.m_Time = time;
        }

        public void BlendToCamera(CinemachineVirtualCamera to, float blendTime)
        {
            this.SetBlendTime(blendTime);
            Singletons.Main.Camera.Brain.ActiveVirtualCamera.Priority = 0;
            to.Priority = 1;
        }

        public void FocusOnTarget(GameObject target, float minBlendTime, float maxBlendtime)
        {
            this._focus.FocusCameraOnTarget(target, minBlendTime, maxBlendtime);
        }

        public void FocusOnTarget(GameObject target)
        {
            this._focus.FocusCameraOnTarget(target, null);
        }
    }
}