using UnityEngine;

namespace ProjectWendigo
{
    public abstract class AInteractable : MonoBehaviour
    {
        public virtual void OnLookAt(GameObject target)
        {
            Singletons.Main.Interface.OpenMessagePanel("- Press F to interact -");
        }

        public virtual void OnLookAway(GameObject target)
        {
            Singletons.Main.Interface.CloseMessagePanel();
        }

        public abstract void OnInteract(GameObject target);
    }
}