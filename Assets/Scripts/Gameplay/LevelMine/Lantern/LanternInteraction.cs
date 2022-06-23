using UnityEngine;

namespace ProjectWendigo
{
    public class LanternInteraction : Pickable
    {
        [SerializeField] private GameObject _focusTarget;

        public override void OnInteract(GameObject target)
        {
            base.OnInteract(target);
            Singletons.Main.Event.Trigger("LanternEvent");
            Singletons.Main.Camera.FocusOnTarget(this._focusTarget);
        }
    }
}