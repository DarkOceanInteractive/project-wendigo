using UnityEngine;

namespace ProjectWendigo
{
    public abstract class AInteractable : MonoBehaviour
    {
        public abstract void OnInteract(GameObject target);
    }
}