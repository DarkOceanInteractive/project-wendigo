using UnityEngine;
using UnityEngine.Events;

namespace ProjectWendigo
{
    public class TriggerBox : MonoBehaviour
    {
        public LayerMask LayerMask;

        public UnityEvent<Collider> OnTriggerBoxEnter;
        public UnityEvent<Collider> OnTriggerBoxEnterOnce;
        public UnityEvent<Collider> OnTriggerBoxStay;
        public UnityEvent<Collider> OnTriggerBoxExit;
        public UnityEvent<Collider> OnTriggerBoxExitOnce;

        private void OnTriggerEnter(Collider other)
        {
            if (!this.LayerMask.Contains(other.gameObject.layer))
                return;

            this.OnTriggerBoxEnter?.Invoke(other);
            if (this.OnTriggerBoxEnterOnce.GetPersistentEventCount() > 0)
            {
                this.OnTriggerBoxEnterOnce.Invoke(other);
                Destroy(this.gameObject);
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (!this.LayerMask.Contains(other.gameObject.layer))
                return;

            this.OnTriggerBoxStay?.Invoke(other);
        }

        private void OnTriggerExit(Collider other)
        {
            if (!this.LayerMask.Contains(other.gameObject.layer))
                return;

            this.OnTriggerBoxExit?.Invoke(other);
            if (this.OnTriggerBoxExitOnce.GetPersistentEventCount() > 0)
            {
                this.OnTriggerBoxExitOnce.Invoke(other);
                Destroy(this.gameObject);
            }
        }
    }
}
