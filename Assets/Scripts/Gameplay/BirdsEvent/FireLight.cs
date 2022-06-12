using System.Collections;
using UnityEngine;

namespace ProjectWendigo
{
    public class FireLight : MonoBehaviour
    {
        [SerializeField] private float _disappearDuration = 1.5f;
        [SerializeField] private float _maxIntensity = 100f;
        [SerializeField] private Vector2Spline _lightIntensityCurve = Vector2Spline.Linear(Vector2.zero, Vector2.one);
        private Light _light;

        private void Awake()
        {
            this._light = this.gameObject.GetComponent<Light>();
        }

        private void OnDrawGizmosSelected()
        {
            this._lightIntensityCurve.DrawGizmos(this.transform.position + Vector3.up - Vector3.right * 0.5f);
        }

        public void TriggerDisappear()
        {
            StartCoroutine(this.Disappear());
        }

        private IEnumerator Disappear()
        {
            float startTime = Time.time;
            float endTime = startTime + this._disappearDuration;

            while (Time.time < endTime)
            {
                this._light.intensity = this._lightIntensityCurve.Interpolate(0f, this._maxIntensity, (Time.time - startTime) / this._disappearDuration);
                yield return new WaitForEndOfFrame();
            }
            Destroy(this.gameObject);
        }
    }
}