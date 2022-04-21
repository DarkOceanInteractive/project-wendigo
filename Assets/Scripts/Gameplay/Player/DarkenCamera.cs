using UnityEngine.Rendering.PostProcessing;
using UnityEngine;

namespace ProjectWendigo
{
    [RequireComponent(typeof(SoundsSpawner))]
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
        private bool _hasVisitedEntrance = false;
        private SoundsSpawner _soundsSpawner;
        //private SoundsSpawner.SoundAmbience _darkSoundAmbience;
        [SerializeField] private float _darknessMinimumVolume = 0.5f;

        public void OnEnterEntrance(Collider collider)
        {
            if (collider.gameObject == this.gameObject)
            {
                if (!this._hasVisitedEntrance)
                {
                    this._hasVisitedEntrance = true;
                    this._soundsSpawner.ActivateAmbience("Entrance");
                }
            }
        }

        public void OnLightEnter(Collider collider)
        {
            if (collider.gameObject == this.gameObject)
            {
                this._isInLight = true;
                this._soundsSpawner.ActivateAmbience("Light");
                this._soundsSpawner.DeactivateAmbience("Dark");
            }
        }

        public void OnLightExit(Collider collider)
        {
            if (collider.gameObject == this.gameObject)
            {
                this._isInLight = false;
                this._soundsSpawner.DeactivateAmbience("Light");
                this._soundsSpawner.ActivateAmbience("Dark");
            }
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
            this._soundsSpawner = this.GetComponent<SoundsSpawner>();
            //this._darkSoundAmbience = ref this._soundsSpawner.FindAmbience("Dark");
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
            this._soundsSpawner.FindAmbience("Dark").Volume = Mathf.Max(_darknessMinimumVolume, intensity / this._maxIntensity);
        }
    }
}
