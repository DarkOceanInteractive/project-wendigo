using UnityEngine;

namespace ProjectWendigo
{
    public class LightLevel : MonoBehaviour
    {
        [SerializeField] [ReadOnly] private float _intensity;
        public float Intensity => this._intensity;
        [SerializeField] private Camera _lightCamera;
        private RenderTexture _lightRenderTexture;
        [Tooltip("Number of updates per second. Higher update rate can have a significant cost on performances.")]
        [SerializeField] private int _updatesPerSecond = 10;
        private float _lastUpdateTime;
        private float _step => 1f / this._updatesPerSecond;

        protected void Start()
        {
            this._lightRenderTexture = this._lightCamera.targetTexture;
            Debug.Assert(this._lightRenderTexture != null, "Light camera must have a render texture attached.");
            this.CalculateLightIntensity();
            this._lastUpdateTime = Time.time;
        }

        protected void OnEnable()
        {
            this._lightCamera.enabled = true;
        }

        protected void OnDisable()
        {
            this._lightCamera.enabled = false;
        }

        protected void Update()
        {
            if (Time.time - this._lastUpdateTime >= this._step)
            {
                this.CalculateLightIntensity();
                this._lastUpdateTime = Time.time;
            }
        }

        protected void CalculateLightIntensity()
        {
            // Get the pixels from the light render texture.
            RenderTexture tmpTexture = RenderTexture.GetTemporary(this._lightRenderTexture.width, this._lightRenderTexture.height, 0, RenderTextureFormat.Default, RenderTextureReadWrite.Linear);
            Graphics.Blit(this._lightRenderTexture, tmpTexture);
            RenderTexture previous = RenderTexture.active;
            RenderTexture.active = tmpTexture;

            Texture2D tmp2dTexture = new Texture2D(this._lightRenderTexture.width, this._lightRenderTexture.height);
            tmp2dTexture.ReadPixels(new Rect(0f, 0f, tmpTexture.width, tmpTexture.height), 0, 0);
            tmp2dTexture.Apply();

            RenderTexture.active = previous;
            RenderTexture.ReleaseTemporary(tmpTexture);

            // Calculate the light intensity from the pixels.
            Color32[] colors = tmp2dTexture.GetPixels32();
            this._intensity = 0f;
            for (int i = 0; i < colors.Length; ++i)
            {
                // This formula gives the intensity of a light from its color.
                this._intensity += 0.2126f * colors[i].r + 0.7152f * colors[i].g + 0.0722f * colors[i].b;
            }
            this._intensity /= colors.Length * 3f;
        }
    }
}
