using UnityEngine;
using UnityEngine.Events;

namespace ProjectWendigo
{
    public class TriggerBox : MonoBehaviour
    {
        public UnityEvent<Collider> OnTriggerBoxEnter;
        public UnityEvent<Collider> OnTriggerBoxEnterOnce;
        public UnityEvent<Collider> OnTriggerBoxStay;
        public UnityEvent<Collider> OnTriggerBoxExit;
        public UnityEvent<Collider> OnTriggerBoxExitOnce;

        private void OnTriggerEnter(Collider other)
        {
            this.OnTriggerBoxEnter?.Invoke(other);
            if (this.OnTriggerBoxEnterOnce.GetPersistentEventCount() > 0)
            {
                this.OnTriggerBoxEnter.Invoke(other);
                Destroy(this.gameObject);
            }
        }

        private void OnTriggerStay(Collider other)
        {
            this.OnTriggerBoxStay?.Invoke(other);
        }

        private void OnTriggerExit(Collider other)
        {
            this.OnTriggerBoxExit?.Invoke(other);
            if (this.OnTriggerBoxExitOnce.GetPersistentEventCount() > 0)
            {
                this.OnTriggerBoxExitOnce.Invoke(other);
                Destroy(this.gameObject);
            }
        }
    }
}
