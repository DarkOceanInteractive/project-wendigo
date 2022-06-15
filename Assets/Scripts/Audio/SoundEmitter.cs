using UnityEngine;

namespace ProjectWendigo
{
    public class SoundEmitter : MonoBehaviour
    {
        public string SoundName;
        public bool PlayOnAwake = true;
        public float PlayAfterDelay = 0f;
        private AudioSource _audioSource;

        public void Awake()
        {
            this._audioSource = Singletons.Main.Sound.AttachAudio(this.gameObject, this.SoundName);
            if (this.PlayAfterDelay != 0f)
                this.PlayDelayed(this.PlayAfterDelay);
            else if (this.PlayOnAwake)
                this.Play();
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