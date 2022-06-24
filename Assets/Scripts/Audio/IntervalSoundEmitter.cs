using UnityEngine;

namespace ProjectWendigo
{
    public class IntervalSoundEmitter : MonoBehaviour
    {
        public string SoundName;
        [Tooltip("Spawn the audio at random intervals between the specified bounds")]
        public Vector2 PlayInterval = Vector2.one;
        [SerializeField, ReadOnly] private float _countDown;
        private AudioSource _audioSource;
        public bool StartOnAwake = true;
        private bool _play = false;

        private void Awake()
        {
            this._audioSource = Singletons.Main.Sound.AttachAudio(this.gameObject, this.SoundName);
            this.ResetCountDown();
            if (this.StartOnAwake)
                this.Play();
        }

        private void ResetCountDown()
        {
            this._countDown = Random.Range(this.PlayInterval.x, this.PlayInterval.y);
        }

        public void Play()
        {
            this._play = true;
        }

        public void Stop()
        {
            this._play = false;
        }

        private void Update()
        {
            if (!this._play)
                return;
            this._countDown -= Time.deltaTime;
            if (this._countDown <= 0)
            {
                if (this._audioSource != null)
                    this._audioSource.Play();
                this.ResetCountDown();
            }
        }
    }
}