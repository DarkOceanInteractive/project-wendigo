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
            public float SpawnFrequency;
            public string[] AudioFiles;
            public Color DebugColor;
            public bool IsActive;
            [ReadOnly] public float StartTime;

            public void SpawnRandomAudio(Vector3 offset)
            {
                Debug.Assert(this.AudioFiles.Length > 0);
                Vector2 pointOnCircle = detail.SoundsSpawnerUtils.GetRandomPointOnCircle(this.Range.x, this.Range.y);
                Vector3 worldPos = offset + new Vector3(pointOnCircle.x, 1f, pointOnCircle.y);
                string audioName = this.AudioFiles[Random.Range(0, this.AudioFiles.Length)];
                Singletons.Main.Sound.PlayAt(audioName, worldPos);
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
                if (state && !ambience.IsActive)
                    ambience.StartTime = Random.Range(0, ambience.SpawnFrequency);
                ambience.IsActive = state;
                Debug.Log($"Ambience {name}: {state}");
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
                this._ambiences[i].StartTime = Random.Range(0, this._ambiences[i].SpawnFrequency);
            }
        }

        protected void Update()
        {
            for (int i = 0; i < this._ambiences.Length; ++i)
            {
                if (!this._ambiences[i].IsActive)
                    continue;
                if (Time.time - this._ambiences[i].StartTime < this._ambiences[i].SpawnFrequency)
                    continue;
                this._ambiences[i].StartTime = Time.time - Random.Range(0, this._ambiences[i].SpawnFrequency - Mathf.Min(0.25f, this._ambiences[i].SpawnFrequency));
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
