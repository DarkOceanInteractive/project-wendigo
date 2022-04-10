using UnityEngine.Rendering.PostProcessing;
using UnityEngine;

namespace ProjectWendigo
{
    public class DarkenCamera : MonoBehaviour
    {
        [Range(0f, 1f)]
        public float Intensity = 0f;
        [SerializeField] private PostProcessProfile _profile;
        private AmbientOcclusion _ao;
        private Grain _grain;
        private Vignette _vignette;

        protected void Awake()
        {
            Debug.Assert(this._profile.TryGetSettings(out this._ao));
            Debug.Assert(this._profile.TryGetSettings(out this._grain));
            Debug.Assert(this._profile.TryGetSettings(out this._vignette));
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

        protected void Start()
        {
            // string s = "";
            // var start = new Vector3(0f, 0f, 0f);
            // var tan1 = new Vector3(-.3f, .7f, 0f);
            // var tan2 = new Vector3(1.3f, .3f, 0f);
            // var end = new Vector3(1f, 1f, 0f);
            // for (float i = 0f; i <= 1f; i += .2f)
            //     s += $"{i} = {Interpolation.CubicBezier(start, tan1, tan2, end, i)}\n";
            // Debug.Log($"{s}");
        }

        protected void Update()
        {
            this.UpdateProfile();
        }

        /// <summary>
        /// Update each profile setting's intensity.
        /// </summary>
        private void UpdateProfile()
        {
            this._ao.intensity.value = this.Intensity;
            this._grain.intensity.value = this.Intensity;
            this._vignette.intensity.value = this.Intensity;
        }
    }
}
