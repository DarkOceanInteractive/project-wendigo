using System.ComponentModel;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering.PostProcessing;
using Cinemachine;

namespace ProjectWendigo
{
    public class OptionsManager : MonoBehaviour
    {
        public UnityEvent<float> OnVolumeChanged;
        public UnityEvent<float> OnBrightnessChanged;

        [SerializeField] private SoundSettingViewModel _soundSettingViewModel;
        [SerializeField] private BrightnessSettingViewModel _brightnessSettingViewModel;
        [SerializeField] private HeadbobbingSettingViewModel _headbobbingSettingViewModel;
        [SerializeField] private InvertYSettingViewModel _invertYSettingViewModel;

        public float Volume
        {
            get => this._soundSettingViewModel.VolumeSetting;
            set => this._soundSettingViewModel.VolumeSetting = value;
        }
        public float Brightness
        {
            get => this._brightnessSettingViewModel.BrightnessSetting;
            set => this._brightnessSettingViewModel.BrightnessSetting = value;
        }
        public bool Headbobbing => this._headbobbingSettingViewModel.HeadbobbingSetting;
        public bool InvertY => this._invertYSettingViewModel.InvertYSetting;

        public void Awake()
        {
            this._soundSettingViewModel.PropertyChanged += (object sender, PropertyChangedEventArgs e) =>
            {
                if (e.PropertyName == nameof(this._soundSettingViewModel.VolumeSetting))
                    this.OnVolumeChanged?.Invoke(this._soundSettingViewModel.VolumeSetting);
            };
            this._brightnessSettingViewModel.PropertyChanged += (object sender, PropertyChangedEventArgs e) =>
            {
                if (e.PropertyName == nameof(this._brightnessSettingViewModel.BrightnessSetting))
                    this.OnBrightnessChanged?.Invoke(this._brightnessSettingViewModel.BrightnessSetting);
            };
        }

        public void OnEnable()
        {
            this.Volume = Singletons.Main.Sound.GetMasterVolume();
        }

        public void UpdateCameraOptions()
        {
            ColorGrading cg;
            Camera.main.gameObject.GetComponent<PostProcessVolume>().profile.TryGetSettings(out cg);
            cg.postExposure.value = this.Brightness;
            Singletons.Main.Camera.PlayerCamera.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.m_InvertInput = !this.InvertY;
        }

        public void EnablePauseMenu(bool enabled = true)
        {
            this.GetComponent<PauseMenu>().enabled = enabled;
        }
    }
}
