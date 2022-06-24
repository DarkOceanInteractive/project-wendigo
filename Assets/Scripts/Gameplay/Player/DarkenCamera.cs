using UnityEngine.Rendering.PostProcessing;
using UnityEngine;

namespace ProjectWendigo
{
    [RequireComponent(typeof(SoundsSpawner))]
    public class DarkenCamera : MonoBehaviour
    {
        [SerializeField] private float _maxIntensity = 0.7f;
        [SerializeField] private float _inLightAnimationSpeedMultiplier = 5f;
        [SerializeField] private float _elapsedTime;
        [SerializeField] private float _transitionDuration;
        private bool _isInLight = true;
        [SerializeField] private Vector2Spline _animationCurve = new Vector2Spline();
        private SoundsSpawner _soundsSpawner;
        [SerializeField] private float _darknessMinimumVolume = 0.5f;

        public void OnLightEnter()
        {
            this._isInLight = true;
            this._soundsSpawner.ActivateAmbience("Light");
            this._soundsSpawner.DeactivateAmbience("Dark");
        }

        public void OnLightExit()
        {
            this._isInLight = false;
            this._soundsSpawner.DeactivateAmbience("Light");
            this._soundsSpawner.ActivateAmbience("Dark");
        }

        public void OnDrawGizmosSelected()
        {
            this._animationCurve.DrawGizmos(this.transform.position + Vector3.up + Vector3.left * 0.5f);
        }

        protected void Awake()
        {
            this._soundsSpawner = this.GetComponent<SoundsSpawner>();
            if (this._isInLight)
                this._elapsedTime = this._transitionDuration;
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
            Singletons.Main.Camera.ChromaticAberration = intensity;
            Singletons.Main.Camera.Grain = intensity;
            Singletons.Main.Camera.Vignette = intensity;
            this._soundsSpawner.FindAmbience("Dark").Volume = Mathf.Max(_darknessMinimumVolume, intensity / this._maxIntensity);
        }
    }
}
