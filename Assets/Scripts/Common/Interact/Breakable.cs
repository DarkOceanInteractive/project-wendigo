using UnityEngine;

namespace ProjectWendigo
{
    public class Breakable : AInteractable
    {
        [SerializeField] private GameObject _obstacle;
        [SerializeField] private GameObject _tool;

        [SerializeField] private Animator _toolAnimator;
        [SerializeField] private string _toolAnimationTrigger;

        [SerializeField] private string _onInteractSFX;

        public override void OnLookAt(GameObject target)
        {
            if (this._tool.activeSelf || this._tool == null)
                Singletons.Main.Interface.OpenMessagePanel("- Press F to break -");
        }

        public override void OnLookAway(GameObject target)
        {
            Singletons.Main.Interface.CloseMessagePanel();
        }

        public override void OnInteract(GameObject target)
        {
            if (this._tool.activeSelf || this._tool == null)
            {
                if (this._toolAnimator != null && this._toolAnimationTrigger != null)
                    this._toolAnimator.SetTrigger(this._toolAnimationTrigger);
                if (this._obstacle.activeSelf)
                    this._obstacle.SetActive(false);
                if (this._onInteractSFX != "")
                    Singletons.Main.Sound.Play(this._onInteractSFX);
            }
        }
    }
}
