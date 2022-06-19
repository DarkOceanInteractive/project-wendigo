using UnityEngine;

namespace ProjectWendigo
{
    public class CarKeyInteraction : AInteractable
    {
        public override void OnLookAt(GameObject target)
        {
            Singletons.Main.Interface.OpenMessagePanel("- Press F to leave -");
        }

        public override void OnInteract(GameObject target)
        {
            Debug.Log("Leave");
        }
    }
}