using UnityEngine;

namespace ProjectWendigo
{
    public class PickableGear : Pickable
    {
        public string RelatedQuestName;

        public override void OnInteract(GameObject target)
        {
            base.OnInteract(target);
            Singletons.Main.Quest.TryUpdateQuestProgress(this.RelatedQuestName, 1);
            Singletons.Main.Interface.CloseMessagePanel();
        }
    }
}