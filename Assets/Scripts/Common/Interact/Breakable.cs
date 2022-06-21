using UnityEngine;

namespace ProjectWendigo
{
    public class Breakable : AInteractable
    {
        [SerializeField] private GameObject _obstacle;
        [SerializeField] private GameObject _tool;

        [SerializeField] private Animator _toolAnimator;
        [SerializeField] private string _toolAnimation;

        [SerializeField] private string _onInteractSFX;

        public override void OnLookAt(GameObject target)
        {
            if (this._tool.activeSelf)
                Singletons.Main.Interface.OpenMessagePanel("- Press F to pick up -");
        }

        public override void OnLookAway(GameObject target)
        {
            Singletons.Main.Interface.CloseMessagePanel();
        }

        public override void OnInteract(GameObject target)
        {
            if (this._tool.activeSelf)
            {
                if (this._toolAnimator != null && this._toolAnimation != null)
                    this._toolAnimator.SetTrigger(this._toolAnimation);
                if (this._obstacle.activeSelf)
                    this._obstacle.SetActive(false);
                if (this._onInteractSFX != "")
                    Singletons.Main.Sound.Play(this._onInteractSFX);
            }
        }
    }
}
