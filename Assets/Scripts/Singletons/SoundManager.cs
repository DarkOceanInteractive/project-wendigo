using System;
using UnityEngine;
using UnityEngine.Audio;

namespace ProjectWendigo
{
    public class SoundManager : MonoBehaviour
    {
        [SerializeField] private Sound[] Sounds;
        [SerializeField] private AudioMixer _mixer;
        [SerializeField] private AudioMixerGroup _masterMixerGroup;

        /// <summary>
        /// Get the master volume value from the attached audio mixer group.
        /// </summary>
        /// <returns>Master volume (in dB)</returns>
        public float GetMasterVolume()
        {
            this._mixer.GetFloat("MasterVolume", out float volume);
            return volume;
        }

        /// <summary>
        /// Set the master volume in the attached audio mixer group.
        /// </summary>
        /// <param name="value">Volume value (in dB)</param>
        public void SetMasterVolume(float value)
        {
            this._mixer.SetFloat("MasterVolume", value);
        }

        /// <summary>
        /// Get an AudioSource for the corresponding sound clip.
        /// </summary>
        /// <param name="name">Sound clip name from the SoundManager sounds array</param>
        /// <returns>New AudioSource with the corresponding sound clip if found</returns>
        public AudioSource GetAudio(string name)
        {
            Sound sound = this.GetSound(name);
            if (sound == null)
                return null;
            return this.SpawnAudio(this.gameObject, sound);
        }

        /// <summary>
        /// Get an AudioSource positioned at a given position in world space.
        /// </summary>
        /// <param name="name">Sound clip name from the SoundManager sounds array</param>
        /// <param name="position">Position in world space where the sound should be emitted from</param>
        /// <param name="spatialBlend">How much the AudioSource is affected by 3D spatialisation calculations</param>
        /// <returns>New AudioSource with the corresponding sound clip if found</returns>
        public AudioSource GetAudioAt(string name, Vector3 position, float spatialBlend = 1f)
        {
            Sound sound = this.GetSound(name);
            if (sound == null)
                return null;
            var soundHandle = new GameObject(name);
            soundHandle.transform.position = position;
            soundHandle.transform.parent = this.transform;
            // Destroying the GameObject after the sound duration assumes
            // the sound will be played directly after this method is called.
            // This impedes some use cases of this method but is generally safer.
            if (!sound.IsLooping)
                Destroy(soundHandle, sound.FullLength);
            AudioSource audioSource = this.SpawnAudio(soundHandle, sound);
            audioSource.spatialBlend = spatialBlend;
            return audioSource;
        }

        /// <summary>
        /// Spawn and attach a new AudioSource on the given object.
        /// </summary>
        /// <param name="handle">GameObject to attach the AudioSource onto</param>
        /// <param name="name">Sound clip name from the SoundManager sounds array</param>
        /// <returns>New AudioSource with the corresponding sound clip if found</returns>
        public AudioSource AttachAudio(GameObject handle, string name, float spatialBlend = 1f)
        {
            Sound sound = this.GetSound(name);
            if (sound == null)
                return null;
            AudioSource audioSource = this.SpawnAudio(handle, sound);
            audioSource.spatialBlend = spatialBlend;
            if (!sound.IsLooping)
                Destroy(audioSource, sound.FullLength);
            return audioSource;
        }

        /// <summary>
        /// Play the corresponding sound in 2D.
        /// </summary>
        /// <param name="name">Sound clip name from the SoundManager sounds array</param>
        public void Play(string name)
        {
            this.GetAudio(name)?.Play();
        }

        /// <summary>
        /// Play the corresponding sound at the given position in world space.
        /// </summary>
        /// <param name="name">Sound clip name from the SoundManager sounds array</param>
        /// <param name="position">Position in world space where the sound should be emitted from</param>
        /// <param name="spatialBlend">How much the AudioSource is affected by 3D spatialisation calculations</param>
        public void PlayAt(string name, Vector3 position, float spatialBlend = 1f)
        {
            this.GetAudioAt(name, position, spatialBlend)?.Play();
        }

        private Sound GetSound(string name)
        {
            Sound sound = Array.Find(this.Sounds, sound => sound.Name == name);
            if (sound == null)
            {
                Debug.LogWarning($"Sound {name} not found.");
                return null;
            }
            return sound;
        }

        private AudioSource SpawnAudio(GameObject gameObject, Sound sound)
        {
            var audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.outputAudioMixerGroup = this._masterMixerGroup;
            audioSource.clip = sound.Clip;
            audioSource.volume = sound.Volume;
            audioSource.pitch = sound.Pitch;
            audioSource.loop = sound.IsLooping;
            if (!sound.IsLooping)
                Destroy(audioSource, sound.FullLength);
            return audioSource;
        }
    }
}
