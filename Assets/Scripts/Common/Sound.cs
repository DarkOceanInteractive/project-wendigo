using System;
using UnityEngine;

namespace ProjectWendigo
{
    [Serializable]
    public class Sound
    {
        public string Name;
        public AudioClip Clip;

        public bool IsLooping = false;
        [Range(0f, 1f)]
        public float Volume = 1f;
        [Range(.1f, 3f)]
        public float Pitch = 1f;

        public float FullLength
        {
            get => this.Clip.length * 1f / this.Pitch;
        }
    }
}
