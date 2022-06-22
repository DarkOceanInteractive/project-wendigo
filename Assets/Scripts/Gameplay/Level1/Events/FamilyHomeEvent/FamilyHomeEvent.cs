using UnityEngine;

namespace ProjectWendigo
{
    public class FamilyHomeEvent : MonoBehaviour
    {
        [SerializeField] private GameObject _daughterFigure;
        [SerializeField] private GameObject _endTriggerBox;

        private void Awake()
        {
            Singletons.Main.Event.On("FamilyHomeEvent", this.StartEvent);
            this._endTriggerBox.SetActive(false);
        }

        public void StartEvent()
        {
            Singletons.Main.Camera.FocusOnTarget(this._daughterFigure);
            Singletons.Main.Camera.BlendVignette(0.4f, 0.3f);
            this._daughterFigure.SetActive(true);
            this._endTriggerBox.SetActive(true);
        }

        public void EndEvent()
        {
            Singletons.Main.Camera.BlendVignette(0f, 0.3f);
            this._daughterFigure.SetActive(false);
        }
    }
}
