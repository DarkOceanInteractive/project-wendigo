using System.Collections;
using System.Linq;
using UnityEngine;

namespace ProjectWendigo
{
    public class FollowRoute : MonoBehaviour
    {
        [SerializeField] private Transform route;
        [SerializeField] private float _translationTime = 1f;

        private Vector3Spline GetRoute()
        {
            Vector3[] controlPoints = this.route.GetComponentsInChildren<Transform>()
                .Skip(1)
                .Select(trs => trs.position)
                .ToArray();
            return new Vector3Spline(controlPoints);
        }

        private void OnDrawGizmos()
        {
            this.GetRoute().DrawGizmos(Vector3.zero);
        }

        public void Go()
        {
            StartCoroutine(GoByTheRoute());
        }

        private IEnumerator GoByTheRoute()
        {
            float startTime = Time.time;
            float endTime = startTime + this._translationTime;
            Vector3Spline route = this.GetRoute();

            while (Time.time < endTime)
            {
                float t = (Time.time - startTime) / this._translationTime;
                transform.position = route.Compute(t);
                yield return new WaitForEndOfFrame();
            }
        }
    }
}