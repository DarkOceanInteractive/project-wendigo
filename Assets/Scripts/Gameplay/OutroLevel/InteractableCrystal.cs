using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectWendigo
{
    public class InteractableCrystal : AInteractable
    {
        [SerializeField] private GameObject crystal;
        [SerializeField] private ParticleSystem powerBeam;
        [SerializeField] private string pickaxeSound;

        public override void OnLookAt(GameObject target)
        {
            var options = new MessagePanelOptions
            {
                Text = $"- Press {Singletons.Main.Input.GetBinding("Player/Interact")} to destroy crystal -",
                Location = MessagePanelLocation.BottomCenter
            };
            Singletons.Main.Interface.OpenMessagePanel(options);
        }

        public override void OnLookAway(GameObject target)
        {

            Singletons.Main.Interface.CloseMessagePanel();
        }

        public override void OnInteract(GameObject target)
        {
            if (this.crystal != null)
                this.crystal.SetActive(false); // Remove crystal object
            if (this.powerBeam.isPlaying)
                powerBeam.Stop(); // Stop the power beam particles that came from the crystal object
            if (this.pickaxeSound != null)
                Singletons.Main.Sound.Play(this.pickaxeSound);
        }
    }
}