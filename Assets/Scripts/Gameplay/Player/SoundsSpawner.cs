using UnityEngine;
using System.Collections.Generic;

namespace ProjectWendigo
{
    namespace detail
    {
        static class SoundsSpawnerUtils
        {
            static public Vector2 CalculatePointOnCircle(float radius, float angle)
            {
                float rad = Mathf.Deg2Rad * angle;
                float x = radius * Mathf.Sin(rad);
                float y = radius * Mathf.Cos(rad);
                return new Vector2(x, y);
            }

            static public Vector2 GetRandomPointOnCircle(float rangeMin, float rangeMax)
            {
                float distance = Random.Range(rangeMin, rangeMax);
                float angle = Random.Range(0, 359);
                Vector2 pointOnCircle = SoundsSpawnerUtils.CalculatePointOnCircle(distance, angle);
                return pointOnCircle;
            }
        }
    }

    public class SoundsSpawner : MonoBehaviour
    {
        [System.Serializable]
        public struct SoundAmbience
        {
            public string Name;
            public Vector2 Range;
            public Vector2 SpawnFrequency;
            [Range(0f, 1f)] public float Volume;
            public string[] AudioFiles;
            public Color DebugColor;
            public bool IsActive;
            [ReadOnly] [SerializeField] public float CountDown;

            public bool ShouldPlay => this.CountDown <= 0f;

            public void UpdateTimer()
            {
                this.CountDown -= Time.deltaTime;
            }

            public void ResetTimer()
            {
                this.CountDown = Random.Range(this.SpawnFrequency.x, this.SpawnFrequency.y * 0.75f);
            }

            public void SpawnRandomAudio(Vector3 offset)
            {
                Debug.Assert(this.AudioFiles.Length > 0);
                Vector2 pointOnCircle = detail.SoundsSpawnerUtils.GetRandomPointOnCircle(this.Range.x, this.Range.y);
                Vector3 worldPos = offset + new Vector3(pointOnCircle.x, 1f, pointOnCircle.y);
                string audioName = this.AudioFiles[Random.Range(0, this.AudioFiles.Length)];
                AudioSource source = Singletons.Main.Sound.GetAudioAt(audioName, worldPos);
                source.volume = this.Volume;
                source.Play();
                this.ResetTimer();
            }
        };

        [SerializeField] private SoundAmbience[] _ambiences;

        public ref SoundAmbience FindAmbience(string name)
        {
            for (int i = 0; i < this._ambiences.Length; ++i)
            {
                if (this._ambiences[i].Name == name)
                    return ref this._ambiences[i];
            }
            throw new System.Exception($"Ambience '{name}' not found.");
        }

        protected bool SetAmbienceActive(string name, bool state)
        {
            try
            {
                ref SoundAmbience ambience = ref this.FindAmbience(name);
                ambience.IsActive = state;
                return true;
            }
            catch (System.Exception e)
            {
                Debug.LogWarning(e.Message);
                return false;
            }
        }

        public static bool operator +(SoundsSpawner soundSpawner, string ambienceName)
        {
            return soundSpawner.SetAmbienceActive(ambienceName, true);
        }

        public static bool operator -(SoundsSpawner soundSpawner, string ambienceName)
        {
            return soundSpawner.SetAmbienceActive(ambienceName, false);
        }

        public void ActivateAmbience(string ambienceName)
        {
            this.SetAmbienceActive(ambienceName, true);
        }

        public void DeactivateAmbience(string ambienceName)
        {
            this.SetAmbienceActive(ambienceName, false);
        }

        protected void Awake()
        {
            for (int i = 0; i < this._ambiences.Length; ++i)
            {
                this._ambiences[i].ResetTimer();
            }
        }

        protected void Update()
        {
            for (int i = 0; i < this._ambiences.Length; ++i)
            {
                if (!this._ambiences[i].IsActive)
                    continue;
                this._ambiences[i].UpdateTimer();
                if (this._ambiences[i].ShouldPlay)
                    this._ambiences[i].SpawnRandomAudio(this.transform.position + Vector3.up);
            }
        }

        protected void OnDrawGizmosSelected()
        {
            for (int i = 0; i < this._ambiences.Length; ++i)
            {
                Gizmos.color = this._ambiences[i].DebugColor;
                Gizmos.DrawSphere(this.transform.position + Vector3.up, this._ambiences[i].Range.x);
                Gizmos.DrawSphere(this.transform.position + Vector3.up, this._ambiences[i].Range.y);
            }
        }
    }
}
