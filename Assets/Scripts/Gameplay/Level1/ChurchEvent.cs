using UnityEngine;
using Cinemachine;

namespace ProjectWendigo
{
    public class ChurchEvent : MonoBehaviour
    {
        private void Awake()
        {
            Singletons.Main.Event.On("ChurchEvent", this.EnterEvent);
        }

        public void EnterEvent()
        {
            this.ChangeSensitivity();
        }

        public void ChangeSensitivity(float sensitivity = 0.005f)
        { // make issue to remind self about better implementation in optionsmanager
            // Call OptionsManager Sensitivity function once for each axis.

            // Optionsmanager Sensitivity functions will look like the below...

            // this.gameObject.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.m_MaxSpeed = sensitivity;
            // this.gameObject.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.m_MaxSpeed = sensitivity;
        }
    }
}
