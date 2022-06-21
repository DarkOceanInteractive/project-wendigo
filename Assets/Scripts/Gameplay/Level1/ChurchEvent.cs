using UnityEngine;
using Cinemachine;

namespace ProjectWendigo
{
    public class ChurchEvent : MonoBehaviour
    {
        [SerializeField] public float Duration = 3f;

        private void Awake()
        {
            Singletons.Main.Event.On("ChurchEvent", this.EnterEvent);
        }

        void Start()
        {
            Singletons.Main.Event.Trigger("ChurchEvent");
        }

        public void EnterEvent()
        {
            var _initialSensitivity = Singletons.Main.Camera.PlayerCamera.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.m_MaxSpeed;
            this.ChangeSensitivity();
            this.GetComponent<NearingBreathing>().Trigger(this.Duration);
            this.Invoke(nameof(this.ChangeSensitivity), this.Duration, _initialSensitivity);
        }

        public void ChangeSensitivity(float sensitivity = 0.005f)
        {
            Singletons.Main.Camera.SetSensitivity(sensitivity);
        }
    }
}
