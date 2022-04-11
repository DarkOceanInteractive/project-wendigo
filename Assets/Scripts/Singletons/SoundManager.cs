using UnityEngine;
using System;

namespace ProjectWendigo
{
    public class SoundManager : MonoBehaviour
    {
        public Sound[] Sounds;

        protected void Awake()
        {
            foreach (Sound sound in this.Sounds)
            {
                sound.Source = this.gameObject.GetComponent<AudioSource>();
                sound.Source.clip = sound.Clip;
                sound.Source.volume = sound.Volume;
                sound.Source.pitch = sound.Pitch;
                sound.Source.loop = sound.IsLooping;
            }
        }

        public void Play(string audioName)
        {
            Sound sound = Array.Find(this.Sounds, sound => sound.Name == audioName);
            if (sound == null)
            {
                Debug.LogWarning($"Sound {audioName} not found.");
                return;
            }
            sound.Source.Play();
        }
    }
}
