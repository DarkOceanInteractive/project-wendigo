using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectWendigo
{
    public class InteractableCrystal : AInteractable
    {
        [SerializeField] private GameObject crystal;
        [SerializeField] private string pickaxeSound;

        public override void OnLookAt(GameObject target)
        {
            Singletons.Main.Interface.OpenMessagePanel("- Press F to destroy crystal -");
        }

        public override void OnLookAway(GameObject target)
        {
            Singletons.Main.Interface.CloseMessagePanel();
        }

        public override void OnInteract(GameObject target)
        {
            if (this.crystal != null)
                this.crystal.SetActive(false);
            if (this.pickaxeSound != null)
                Singletons.Main.Sound.Play(this.pickaxeSound);
        }
    }
}