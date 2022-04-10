using UnityEngine.Rendering.PostProcessing;
using UnityEngine;

namespace ProjectWendigo
{
    [RequireComponent(typeof(LightLevel))]
    public class DarkenCamera : MonoBehaviour
    {
        [SerializeField] private PostProcessProfile _profile;
        [SerializeField] private float _lightIntensityLowerBound;
        [SerializeField] private float _lightIntensityUpperBound;
        [SerializeField] private float _maxIntensity = 0.7f;
        private LightLevel _lightLevel;
        private AmbientOcclusion _ao;
        private Grain _grain;
        private Vignette _vignette;

        protected void Awake()
        {
            Debug.Assert(this._profile.TryGetSettings(out this._ao));
            Debug.Assert(this._profile.TryGetSettings(out this._grain));
            Debug.Assert(this._profile.TryGetSettings(out this._vignette));
            this._lightLevel = this.GetComponent<LightLevel>();
        }

        protected void OnEnable()
        {
            this._ao.enabled.value = true;
            this._grain.enabled.value = true;
            this._vignette.enabled.value = true;
        }

        protected void OnDisable()
        {
            this._ao.enabled.value = false;
            this._grain.enabled.value = false;
            this._vignette.enabled.value = false;
        }

        protected void Update()
        {
            float lightIntensity = this._lightLevel.Intensity;
            float relativeIntensity = (lightIntensity - this._lightIntensityLowerBound) / (this._lightIntensityUpperBound - this._lightIntensityLowerBound);
            relativeIntensity = 1f - Mathf.Clamp(relativeIntensity, 0f, 1f);
            this.UpdateProfile(relativeIntensity * this._maxIntensity);
        }

        /// <summary>
        /// Update each profile setting's intensity.
        /// </summary>
        private void UpdateProfile(float intensity)
        {
            this._ao.intensity.value = intensity;
            this._grain.intensity.value = intensity;
            this._vignette.intensity.value = intensity;
        }
    }
}
