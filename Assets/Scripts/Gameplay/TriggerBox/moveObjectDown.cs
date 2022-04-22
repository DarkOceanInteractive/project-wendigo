using UnityEngine;
using System.Collections;

namespace ProjectWendigo
{
    public class moveObjectDown : MonoBehaviour
    {
        [SerializeField] private Vector3 _offset;
        [SerializeField] private float _animationDuration;
        [SerializeField] private Vector2Spline _animationCurve = new Vector2Spline();
        private Vector3 _startPosition;
        private float _animationStartTime;
        private bool _hasBeenTriggered = false;

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
            if (this._hasBeenTriggered || other.tag != "Player")
                return;
            this._hasBeenTriggered = true;
            this._animationStartTime = Time.time;
            this.StartCoroutine("Fall");
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
                yield return null;
            }
            this.transform.position = this._startPosition + this._offset;
        }
    }
}