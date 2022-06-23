using UnityEngine;
using Cinemachine;

namespace ProjectWendigo
{
    public class ChurchEvent : MonoBehaviour
    {
        [SerializeField] public float Duration = 3f;

        [SerializeField] private GameObject _churchStatue;
        [SerializeField] private string _ambientSoundEffectName;
        [SerializeField] private float _ambientSoundEffectVolume = 0.1f;

        private void Awake()
        {
            Singletons.Main.Event.On("ChurchEvent", this.EnterEvent);
        }

        void Start()
        {
            if (this._ambientSoundEffectName != "")
            {
                AudioSource audio = Singletons.Main.Sound.GetAudioAt(this._ambientSoundEffectName, this._churchStatue.transform.position);
                audio.volume *= this._ambientSoundEffectVolume;
                audio.Play();
            }
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
