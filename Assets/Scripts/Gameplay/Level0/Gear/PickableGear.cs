using UnityEngine;

namespace ProjectWendigo
{
    public class PickableGear : Pickable
    {
        public string RelatedQuestName;

        private bool _wasPickedUp = false;

        public override void OnInteract(GameObject target)
        {
            base.OnInteract(target);
            if (!this._wasPickedUp)
            {
                Singletons.Main.Quest.TryUpdateQuestProgress(this.RelatedQuestName, 1);
                Singletons.Main.Interface.CloseMessagePanel();
            }
        }
    }
}