using System.ComponentModel;
using UnityEngine;
using UnityEngine.Events;

namespace ProjectWendigo
{
    public class OptionsManager : MonoBehaviour
    {
        public UnityEvent<float> OnVolumeChanged;
        public UnityEvent<float> OnBrightnessChanged;
        public UnityEvent<float> OnSensitivityChanged;
        public UnityEvent<bool> OnInvertYChanged;
        public UnityEvent<bool> OnHeadbobbingChanged;
        [SerializeField] private SoundSettingViewModel _soundSettingViewModel;
        [SerializeField] private BrightnessSettingViewModel _brightnessSettingViewModel;
        [SerializeField] private SensitivitySettingViewModel _sensitivitySettingViewModel;
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
        public float Sensitivity
        {
            get => this._sensitivitySettingViewModel.SensitivitySetting;
            set => this._sensitivitySettingViewModel.SensitivitySetting = value;
        }
        public bool Headbobbing => this._headbobbingSettingViewModel.HeadbobbingSetting;
        public bool InvertY => this._invertYSettingViewModel.InvertYSetting;

        public void Start()
        {
            this._soundSettingViewModel.PropertyChanged += (object sender, PropertyChangedEventArgs e) =>
            {
                if (e.PropertyName == nameof(this._soundSettingViewModel.VolumeSetting))
                    this.OnVolumeChanged?.Invoke(this._soundSettingViewModel.VolumeSetting);
            };
            this.OnVolumeChanged?.Invoke(this._soundSettingViewModel.VolumeSetting);
            this._brightnessSettingViewModel.PropertyChanged += (object sender, PropertyChangedEventArgs e) =>
            {
                if (e.PropertyName == nameof(this._brightnessSettingViewModel.BrightnessSetting))
                    this.OnBrightnessChanged?.Invoke(this._brightnessSettingViewModel.BrightnessSetting);
            };
            this.OnBrightnessChanged?.Invoke(this._brightnessSettingViewModel.BrightnessSetting);
            this._sensitivitySettingViewModel.PropertyChanged += (object sender, PropertyChangedEventArgs e) =>
            {
                if (e.PropertyName == nameof(this._sensitivitySettingViewModel.SensitivitySetting))
                    this.OnSensitivityChanged?.Invoke(this._sensitivitySettingViewModel.SensitivitySetting);
            };
            this.OnSensitivityChanged?.Invoke(this._sensitivitySettingViewModel.SensitivitySetting);
            this._headbobbingSettingViewModel.PropertyChanged += (object sender, PropertyChangedEventArgs e) =>
            {
                if (e.PropertyName == nameof(this._headbobbingSettingViewModel.HeadbobbingSetting))
                    this.OnHeadbobbingChanged?.Invoke(this._headbobbingSettingViewModel.HeadbobbingSetting);
            };
            this.OnHeadbobbingChanged?.Invoke(this._headbobbingSettingViewModel.HeadbobbingSetting);
            this._invertYSettingViewModel.PropertyChanged += (object sender, PropertyChangedEventArgs e) =>
            {
                if (e.PropertyName == nameof(this._invertYSettingViewModel.InvertYSetting))
                    this.OnInvertYChanged?.Invoke(this._invertYSettingViewModel.InvertYSetting);
            };
            this.OnInvertYChanged?.Invoke(this._invertYSettingViewModel.InvertYSetting);
        }

        public void EnablePauseMenu(bool enabled = true)
        {
            this.GetComponent<PauseMenu>().enabled = enabled;
        }
    }
}
