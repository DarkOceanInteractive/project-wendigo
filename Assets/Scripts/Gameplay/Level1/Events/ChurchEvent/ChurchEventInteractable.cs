using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectWendigo
{
    public class ChurchEventInteractable : AInteractable
    {
        public override void OnLookAt(GameObject target)
        {
            var options = new MessagePanelOptions
            {
                Text = $"- Press {Singletons.Main.Input.GetBinding("Player/Interact")} to interact -",
                Location = MessagePanelLocation.BottomCenter
            };
            Singletons.Main.Interface.OpenMessagePanel(options);
        }

        public override void OnInteract(GameObject target)
        {
            Singletons.Main.Event.Trigger("ChurchEvent");
        }
    }
}