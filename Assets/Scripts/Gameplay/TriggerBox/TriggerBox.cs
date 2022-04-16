using UnityEngine;
using UnityEngine.Events;

namespace ProjectWendigo
{
    public class TriggerBox : MonoBehaviour
    {
        public UnityEvent<Collider> OnTriggerBoxEnter;
        public UnityEvent<Collider> OnTriggerBoxStay;
        public UnityEvent<Collider> OnTriggerBoxExit;

        private void OnTriggerEnter(Collider other)
        {
            this.OnTriggerBoxEnter?.Invoke(other);
        }

        private void OnTriggerStay(Collider other)
        {
            this.OnTriggerBoxStay?.Invoke(other);
        }

        private void OnTriggerExit(Collider other)
        {
            this.OnTriggerBoxExit?.Invoke(other);
        }
    }
}
