using System.Collections;
using UnityEngine;

namespace ProjectWendigo
{
    public class DwightInteraction : Pickable
    {
        [SerializeField] private string _archiveEntryTitle;
        [SerializeField] private Transform _baseTransform;
        [SerializeField] private Transform _targetTransform;
        [SerializeField] private Vector2Spline _spline = Vector2Spline.Linear(Vector2.zero, Vector2.one);
        [SerializeField] private float _animationTime = 1.5f;
        private bool _isAnimating = false;
        private bool _isLookedAt = false;

        private void OnDrawGizmos()
        {
            this._spline.DrawGizmos(this.transform.position + Vector3.up - Vector3.right * 0.5f);
        }

        public IEnumerator RotateAnimation()
        {
            float elapsedTime = this._isLookedAt ? 0f : this._animationTime;
            bool isStarting = true;
            this._isAnimating = true;
            while (isStarting || (elapsedTime > 0f && elapsedTime < this._animationTime))
            {
                isStarting = false;
                float multiplier = this._isLookedAt ? 1 : -1f;
                elapsedTime = Mathf.Clamp(elapsedTime + multiplier * Time.deltaTime, 0f, this._animationTime);
                float t = this._spline.Interpolate(0f, 1f, elapsedTime / this._animationTime);
                this.transform.localRotation = Quaternion.Lerp(this._baseTransform.localRotation, this._targetTransform.localRotation, t);
                yield return new WaitForEndOfFrame();
            }
            this._isAnimating = false;
        }

        public override void OnLookAt(GameObject target)
        {
            this._isLookedAt = true;
            if (!this._isAnimating)
                StartCoroutine(this.RotateAnimation());
        }

        public override void OnLookAway(GameObject target)
        {
            this._isLookedAt = false;
            if (!this._isAnimating && this.isActiveAndEnabled)
                StartCoroutine(this.RotateAnimation());
        }

        public override void OnInteract(GameObject target)
        {
            base.OnInteract(target);
            Singletons.Main.Notebook.AddArchiveEntryByTitle(this._archiveEntryTitle);
        }
    }
}