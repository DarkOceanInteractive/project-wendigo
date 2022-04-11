using System;
using UnityEngine;
using UnityEngine.Audio;

namespace ProjectWendigo
{
    [Serializable]
    public class Sound
    {
        public string Name;
        public bool IsLooping = false;
        [Range(0f, 1f)]
        public float Volume = 1f;
        [Range(0f, 1f)]
        public float Pitch = 0f;

        public AudioClip Clip;
        [HideInInspector]
        public AudioSource Source;
    }
}
