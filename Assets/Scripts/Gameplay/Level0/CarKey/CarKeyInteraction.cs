using UnityEngine;

namespace ProjectWendigo
{
    public class CarKeyInteraction : AInteractable
    {
        [SerializeField] private string _onInteractSoundName;
        [SerializeField] private float _onInteractSoundVolume = 1.0f;

        public override void OnLookAt(GameObject target)
        {
            var options = new MessagePanelOptions
            {
                Text = $"- Press {Singletons.Main.Input.GetBinding("Player/Interact")} to leave -",
                Location = MessagePanelLocation.BottomCenter
            };
            Singletons.Main.Interface.OpenMessagePanel(options);
        }

        public override void OnInteract(GameObject target)
        {
            if (this._onInteractSoundName != "")
            {
                AudioSource audio = Singletons.Main.Sound.GetAudio(this._onInteractSoundName);
                audio.volume *= this._onInteractSoundVolume;
                audio.Play();
            }
            Singletons.Main.Fade.FadeOutEffect();
            this.Invoke(nameof(this.GoToNextScene), 3f);
        }

        public void GoToNextScene()
        {
            Singletons.Main.Scene.GoToNextScene();
        }
    }
}