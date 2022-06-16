using UnityEngine;

namespace ProjectWendigo
{
    public class Pickable : AInteractable
    {
        [SerializeField] private GameObject _inWorldItme;
        [SerializeField] private GameObject _inHandItem;

        public override void OnLookAt(GameObject target)
        {
            Singletons.Main.Interface.OpenMessagePanel("- Press F to pick up -");
        }

        public override void OnLookAway(GameObject target)
        {
            Singletons.Main.Interface.CloseMessagePanel();
        }

        public override void OnInteract(GameObject target)
        {
            if (this._inWorldItme != null)
                this._inWorldItme.SetActive(false);
            if (this._inHandItem != null)
                this._inHandItem.SetActive(true);
        }
    }
}