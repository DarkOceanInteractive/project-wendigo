using UnityEngine;

namespace ProjectWendigo
{
    public class PickableLantern : AInteractable
    {
        [SerializeField] private GameObject _inWorldLantern;
        [SerializeField] private GameObject _inHandLantern;

        public override void OnInteract(GameObject target)
        {
            this._inHandLantern.SetActive(true);
            this._inWorldLantern.SetActive(false);
            LevelMineStateContext.Instance.EnterLanternEvent();
        }
    }
}