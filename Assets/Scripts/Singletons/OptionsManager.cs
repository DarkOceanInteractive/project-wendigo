using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.Events;
using Cinemachine;

namespace ProjectWendigo
{
    public class OptionsManager : MonoBehaviour
    {
        [SerializeField] private SoundSettingViewModel _soundSettingViewModel;
        [SerializeField] private BrightnessSettingViewModel _brightnessSettingViewModel;
        [SerializeField] private HeadbobbingSettingViewModel _headbobbingSettingViewModel;
        [SerializeField] private InvertYSettingViewModel _invertYSettingViewModel;

        public float Volume => _soundSettingViewModel.VolumeSetting;
        public float Brightness => _brightnessSettingViewModel.BrightnessSetting;
        public bool Headbobbing => _headbobbingSettingViewModel.HeadbobbingSetting;
        public bool InvertY => _invertYSettingViewModel.InvertYSetting;

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
