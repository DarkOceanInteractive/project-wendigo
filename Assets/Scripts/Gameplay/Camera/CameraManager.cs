using System.Collections;
using System.Reflection;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine;
using Cinemachine;

namespace ProjectWendigo
{
    public class CameraManager : MonoBehaviour
    {
        public Camera Main => Camera.main;
        public CinemachineBrain Brain;
        public CinemachineVirtualCamera PlayerCamera;
        public CinemachineVirtualCamera PlayerFocusCamera;
        private CameraFocus _focus;
        private Volume _postProcessVolume;

        private void Awake()
        {
            Debug.Assert(this.Brain != null || Camera.main.TryGetComponent(out this.Brain));
            Debug.Assert(this._focus != null || this.TryGetComponent(out this._focus));
            Debug.Assert(this.Main.TryGetComponent(out this._postProcessVolume));
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

        private Effect GetPostProcessEffect<Effect>()
            where Effect : VolumeComponent
        {
            if (this._postProcessVolume.profile.TryGet(out Effect effect))
                return effect;
            Debug.LogWarning($"No {typeof(Effect).Name} component found in the main camera profile");
            return null;
        }

        private IEnumerator BlendEffect(PropertyInfo property, float targetValue, float time)
        {
            float baseValue = (float)property.GetValue(this);
            float elapsedTime = 0f;
            while (elapsedTime < time)
            {
                property.SetValue(this, Mathf.Lerp(baseValue, targetValue, elapsedTime / time));
                yield return new WaitForEndOfFrame();
                elapsedTime += Time.deltaTime;
            }
        }

        public float Brightness
        {
            get => this.GetPostProcessEffect<ColorAdjustments>().postExposure.value;
            set => this.GetPostProcessEffect<ColorAdjustments>().postExposure.value = value;
        }
        public void BlendBrightness(float targetValue, float time)
        {
            StartCoroutine(this.BlendEffect(typeof(CameraManager).GetProperty(nameof(this.Brightness)), targetValue, time));
        }

        public float Vignette
        {
            get => this.GetPostProcessEffect<Vignette>().intensity.value;
            set => this.GetPostProcessEffect<Vignette>().intensity.value = value;
        }
        public void BlendVignette(float targetValue, float time)
        {
            StartCoroutine(this.BlendEffect(typeof(CameraManager).GetProperty(nameof(this.Vignette)), targetValue, time));
        }

        public float Grain
        {
            get => this.GetPostProcessEffect<FilmGrain>().intensity.value;
            set => this.GetPostProcessEffect<FilmGrain>().intensity.value = value;
        }
        public void BlendGrain(float targetValue, float time)
        {
            StartCoroutine(this.BlendEffect(typeof(CameraManager).GetProperty(nameof(this.Grain)), targetValue, time));
        }

        public float ChromaticAberration
        {
            get => this.GetPostProcessEffect<ChromaticAberration>().intensity.value;
            set => this.GetPostProcessEffect<ChromaticAberration>().intensity.value = value;
        }
        public void BlendChromaticAberration(float targetValue, float time)
        {
            StartCoroutine(this.BlendEffect(typeof(CameraManager).GetProperty(nameof(this.ChromaticAberration)), targetValue, time));
        }

        public void SetInvertYAxis(bool invert = true)
        {
            this.PlayerCamera.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.m_InvertInput = !invert;
        }

        public void SetSensitivity(float verticalAxis, float horizontalAxis)
        {
            this.PlayerCamera.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.m_MaxSpeed = verticalAxis;
            this.PlayerCamera.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.m_MaxSpeed = horizontalAxis;
        }
        public void SetSensitivity(float sensitivity)
        {
            this.SetSensitivity(sensitivity, sensitivity);
        }
    }
}