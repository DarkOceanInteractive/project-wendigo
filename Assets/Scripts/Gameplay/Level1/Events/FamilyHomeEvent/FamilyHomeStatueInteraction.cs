using UnityEngine;

namespace ProjectWendigo
{
    public class FamilyHomeStatueInteraction : Pickable
    {
        public override void OnInteract(GameObject target)
        {
            base.OnInteract(target);
            Singletons.Main.Event.Trigger("FamilyHomeEvent");
        }
    }
}