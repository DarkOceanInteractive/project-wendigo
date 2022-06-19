using UnityEngine;

namespace ProjectWendigo
{
    public class Pickable : AInteractable
    {
        [SerializeField] private GameObject _inWorldItem;
        [SerializeField] private GameObject _inHandItem;
        [SerializeField] private string _onPickUpSoundName;

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
            if (this._inWorldItem != null)
                this._inWorldItem.SetActive(false);
            if (this._inHandItem != null)
                this._inHandItem.SetActive(true);
            if (this._onPickUpSoundName != "")
                Singletons.Main.Sound.Play(this._onPickUpSoundName);
        }
    }
}