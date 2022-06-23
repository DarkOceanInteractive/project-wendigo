using UnityEngine;

namespace ProjectWendigo
{
    public class CarKeyInteraction : AInteractable
    {
        public override void OnLookAt(GameObject target)
        {
            var options = new MessagePanelOptions
            {
                Text = $"- Press {Singletons.Main.Input.GetBinding("Player/Interact")} to leave -",
                Location = MessagePanelLocation.BottomCenter
            };
            Singletons.Main.Interface.OpenMessagePanel(options);
        }

        public override void OnInteract(GameObject target)
        {
            Singletons.Main.Scene.GoToNextScene();
        }
    }
}