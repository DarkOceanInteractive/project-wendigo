using UnityEngine;

namespace ProjectWendigo
{
    public abstract class AInteractable : MonoBehaviour
    {
        public virtual void OnLookAt(GameObject target)
        {
            var options = new MessagePanelOptions
            {
                Text = $"- Press {Singletons.Main.Input.GetBinding("Player/Interact")} to interact -",
                Location = MessagePanelLocation.BottomCenter
            };
            Singletons.Main.Interface.OpenMessagePanel(options);
        }

        public virtual void OnLookAway(GameObject target)
        {
            Singletons.Main.Interface.CloseMessagePanel();
        }

        public abstract void OnInteract(GameObject target);
    }
}