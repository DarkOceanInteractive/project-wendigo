using System.ComponentModel;
using UnityEngine;
using UnityEngine.Events;
using Cinemachine;

namespace ProjectWendigo
{
    public class OptionsManager : MonoBehaviour
    {
        public UnityEvent<float> OnVolumeChanged;
        public UnityEvent<float> OnBrightnessChanged;
        public UnityEvent<bool> OnInvertYChanged;
        public UnityEvent<bool> OnHeadbobbingChanged;
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
