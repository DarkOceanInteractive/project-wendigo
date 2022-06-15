using UnityEngine;

namespace ProjectWendigo
{
    /// <summary>
    /// When the player picks up the statue in the level 1 church, creeping noises start
    /// closing up the distance in their back before fading away.
    /// </summary>
    public class NearingBreathing : MonoBehaviour
    {
        [SerializeField] private string _soundName;
        [SerializeField] private float _spawnDistance = 5f;
        [SerializeField] private float _time = 3f;
        [SerializeField] private Vector2Spline _spline = Vector2Spline.Linear(Vector2.zero, Vector2.one);
        private GameObject _audioHandle = null;
        private Vector3 _startPosition;
        private float _startTime;

        private void OnDrawGizmosSelected()
        {
            this._spline.DrawGizmos(this.transform.position + Vector3.up - Vector3.right * 0.5f);
        }

        public void Trigger()
        {
            Vector3 cameraDirection = Camera.main.transform.forward;
            this._startPosition = Singletons.Main.Player.PlayerBody.transform.position - cameraDirection * this._spawnDistance;
            this._startTime = Time.time;
            this._audioHandle = new GameObject("NearingBreathing");
            this._audioHandle.transform.position = this._startPosition;
            Singletons.Main.Sound.AttachAudio(this._audioHandle, this._soundName).Play();
        }

        private void Update()
        {
            if (this._audioHandle == null)
                return;

            float t = (Time.time - this._startTime) / this._time;
            if (t <= 1f)
            {
                Vector3 position = this._spline.Interpolate(this._startPosition, Singletons.Main.Player.PlayerBody.transform.position, t);
                this._audioHandle.transform.position = position;
            }
            else
            {
                Destroy(this._audioHandle);
                this._audioHandle = null;
            }
        }
    }
}