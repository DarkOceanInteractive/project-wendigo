using UnityEngine;

namespace ProjectWendigo
{
    public class LanternInteraction : Pickable
    {
        public override void OnInteract(GameObject target)
        {
            base.OnInteract(target);
            LevelMineStateContext.Instance.EnterLanternEvent();
            Singletons.Main.Event.Trigger("LanternEvent");
        }
    }
}