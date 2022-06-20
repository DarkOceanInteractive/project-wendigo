using UnityEngine;

namespace ProjectWendigo
{
    public class Pickable : AInteractable
    {
        [SerializeField] protected GameObject _inWorldItem;
        [SerializeField] protected GameObject _inHandItem;
        [SerializeField] protected string _onPickUpSoundName;

        public override void OnLookAt(GameObject target)
        {
            var options = new MessagePanelOptions
            {
                Text = $"- Press {Singletons.Main.Input.GetBinding("Player/Interact")} to pick up -",
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
            if (this._inWorldItem != null)
                this._inWorldItem.SetActive(false);
            if (this._inHandItem != null)
                this._inHandItem.SetActive(true);
            if (this._onPickUpSoundName != "")
                Singletons.Main.Sound.Play(this._onPickUpSoundName);
        }
    }
}