using UnityEngine;
using Cinemachine;
using System.Collections;

namespace ProjectWendigo
{
    public class RockFallAnimation : MonoBehaviour
    {
        [SerializeField] private Vector3 _offset;
        [SerializeField] private float _animationDuration;
        [SerializeField] private Vector2Spline _animationCurve = new Vector2Spline();
        private Vector3 _startPosition;
        private float _animationStartTime;
        private bool _hasBeenTriggered = false;
        private GameObject _audioSourceObject;
        [SerializeField] private string _soundName;

        protected void Start()
        {
            this._startPosition = this.transform.position;
        }

        protected void OnDrawGizmosSelected()
        {
            this._animationCurve.DrawGizmos(this.transform.position);
        }

        public void OnTriggerBoxEnter(Collider other)
        {
            // This only applies when in the earthquake event
            Debug.Log(LevelMineStateContext.Instance.IsInState<LevelMineStates.Earthquake>());
            if (!LevelMineStateContext.Instance.IsInState<LevelMineStates.Earthquake>())
                return;
            Debug.Log("Triggering");
            if (this._hasBeenTriggered || other.tag != "Player")
                return;
            this._hasBeenTriggered = true;
            this._animationStartTime = Time.time;
            this.PlaySound();
            this.GetComponent<CinemachineImpulseSource>()?.GenerateImpulse();
            this.StartCoroutine("Fall");
        }

        private void PlaySound()
        {
            if (this._soundName != null && this._soundName != "")
            {
                AudioSource audioSource = Singletons.Main.Sound.GetAudioAt(this._soundName, this.transform.position);
                this._audioSourceObject = audioSource.gameObject;
                audioSource.Play();
            }
        }

        private IEnumerator Fall()
        {
            float elapsedTime = Time.time - this._animationStartTime;
            while (elapsedTime <= this._animationDuration)
            {
                elapsedTime = Time.time - this._animationStartTime;
                float t = elapsedTime / this._animationDuration;
                Vector3 currentOffset = this._animationCurve.Interpolate(Vector3.zero, this._offset, t);
                this.transform.position = this._startPosition + currentOffset;
                if (this._audioSourceObject != null)
                    this._audioSourceObject.transform.position = this._startPosition + currentOffset;
                yield return null;
            }
            this.transform.position = this._startPosition + this._offset;
        }
    }
}