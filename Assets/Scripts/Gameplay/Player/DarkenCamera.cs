using UnityEngine.Rendering.PostProcessing;
using UnityEngine;

namespace ProjectWendigo
{
    public class DarkenCamera : MonoBehaviour
    {
        [SerializeField] private PostProcessProfile _profile;
        [SerializeField] private float _maxIntensity = 0.7f;
        [SerializeField] private float _inLightAnimationSpeedMultiplier = 5f;
        [SerializeField] private float _elapsedTime;
        [SerializeField] private float _transitionDuration;
        private bool _isInLight = true;
        [SerializeField] private Vector2Spline _animationCurve = new Vector2Spline();
        private AmbientOcclusion _ao;
        private Grain _grain;
        private Vignette _vignette;

        public void OnLightEnter()
        {
            this._isInLight = true;
        }

        public void OnLightExit()
        {
            this._isInLight = false;
        }

        public void OnDrawGizmosSelected()
        {
            this._animationCurve.DrawGizmos(this.transform.position + Vector3.up + Vector3.left * 0.5f);
        }

        protected void Awake()
        {
            Debug.Assert(this._profile.TryGetSettings(out this._ao));
            Debug.Assert(this._profile.TryGetSettings(out this._grain));
            Debug.Assert(this._profile.TryGetSettings(out this._vignette));
            if (this._isInLight)
                this._elapsedTime = this._transitionDuration;
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
            float multiplier = this._isInLight ? _inLightAnimationSpeedMultiplier : -1f;
            this._elapsedTime = Mathf.Clamp(this._elapsedTime + multiplier * Time.deltaTime, 0f, this._transitionDuration);
            float lightIntensity = this._animationCurve.Interpolate(0f, 1f, this._elapsedTime / this._transitionDuration);
            this.UpdateProfile((1f - lightIntensity) * this._maxIntensity);
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
