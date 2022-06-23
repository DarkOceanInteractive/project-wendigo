using UnityEngine;

namespace ProjectWendigo
{
    public class SpawnFireEvent : MonoBehaviour
    {
        [SerializeField] private GameObject[] _crystals;
        [SerializeField] private ParticleSystem[] _fireParticles;
        [SerializeField] private ParticleSystem _fireBarrier;

        [SerializeField] private string _ambientSoundEffectName;
        [SerializeField] private float _ambientSoundEffectVolume = 0.2f;

        private void Start()
        {
            if (this._ambientSoundEffectName != "")
                {
                    AudioSource audio = Singletons.Main.Sound.GetAudio(this._ambientSoundEffectName);
                    audio.volume *= this._ambientSoundEffectVolume;
                    audio.Play();
                }
            // Stop particles 
            for (int i = 0; i < this._fireParticles.Length; i++)
            {
                this._fireParticles[i].Stop();
            }
            // Start fire particle barrier
            this._fireBarrier.Play();
            foreach (GameObject crystal in this._crystals)
                crystal.SetInteractable();
        }

        // Method to stop the firebarrier and start playing the fire particles
        public void PlayFire()
        {
            for (int i = 0; i < this._fireParticles.Length; i++)
            {
                if (!this._fireParticles[i].isPlaying)
                    this._fireParticles[i].Play();
            }

            if (this._fireBarrier.isPlaying)
                this._fireBarrier.Stop();
        }
    }
}