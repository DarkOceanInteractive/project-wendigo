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

        private void Awake()
        {
            this._audioSource = Singletons.Main.Sound.AttachAudio(this.gameObject, this.SoundName);
            this.ResetCountDown();
        }

        private void ResetCountDown()
        {
            this._countDown = Random.Range(this.PlayInterval.x, this.PlayInterval.y);
        }

        private void Update()
        {
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