using UnityEngine;

namespace ProjectWendigo
{
    public class SoundEmitter : MonoBehaviour
    {
        public string SoundName;
        [Tooltip("Play sound on awake, overriding other settings")]
        public bool PlayOnAwake = true;
        [Tooltip("Play the audio automatically after specified delay if other than 0")]
        public float PlayAfterDelay = 0f;
        public float Volume = 1f;
        private AudioSource _audioSource;

        private void Awake()
        {
            this._audioSource = Singletons.Main.Sound.AttachAudio(this.gameObject, this.SoundName);
            this._audioSource.volume *= this.Volume;
            if (this.PlayOnAwake)
                this.Play();
            else if (this.PlayAfterDelay != 0f)
                this.PlayDelayed(this.PlayAfterDelay);
        }

        public void Play()
        {
            this._audioSource.Play();
        }

        public void PlayDelayed(float delay)
        {
            this._audioSource.PlayDelayed(delay);
        }
    }
}