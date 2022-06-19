using UnityEngine;

namespace ProjectWendigo
{
    public static class SetObjectInteractable
    {
        public static void SetInteractable(this GameObject target)
        {
            target.layer = LayerMask.NameToLayer("Interactable");
        }

        public static void SetInteractable(this GameObject target, out int previousLayer)
        {
            previousLayer = target.layer;
            target.layer = LayerMask.NameToLayer("Interactable");
        }
    }
}