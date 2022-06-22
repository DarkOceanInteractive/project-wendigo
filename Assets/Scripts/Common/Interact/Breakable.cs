using UnityEngine;

namespace ProjectWendigo
{
    public class Breakable : AInteractable
    {
        [SerializeField] private GameObject _obstacle;
        [SerializeField] private GameObject _tool;

        [SerializeField] private Animator _toolAnimator;
        [SerializeField] private string _toolAnimationTrigger;

        [SerializeField] private string _onBreakSoundName;
        [SerializeField] private float _onBreakSoundVolume = 0.1f;
        public string RelatedQuestName;

        public override void OnLookAt(GameObject target)
        {
            if (this._tool.activeSelf || this._tool == null)
            {
                var options = new MessagePanelOptions
                {
                    Text = $"- Press {Singletons.Main.Input.GetBinding("Player/Interact")} to break -",
                    Location = MessagePanelLocation.BottomCenter
                };
                Singletons.Main.Interface.OpenMessagePanel(options);
            }
        }

        public override void OnLookAway(GameObject target)
        {
            Singletons.Main.Interface.CloseMessagePanel();
        }

        public override void OnInteract(GameObject target)
        {
            if (this._tool.activeSelf || this._tool == null)
            {
                if (this.RelatedQuestName != "")
                    Singletons.Main.Quest.TryUpdateQuestProgress(this.RelatedQuestName, 1);
                if (this._toolAnimator != null && this._toolAnimationTrigger != null)
                    this._toolAnimator.SetTrigger(this._toolAnimationTrigger);
                if (this._obstacle.activeSelf)
                    this._obstacle.SetActive(false);
                if (this._onBreakSoundName != "")
                {
                    AudioSource audio = Singletons.Main.Sound.GetAudio(this._onBreakSoundName);
                    audio.volume *= this._onBreakSoundVolume;
                    audio.Play();
                }
            }
        }
    }
}
