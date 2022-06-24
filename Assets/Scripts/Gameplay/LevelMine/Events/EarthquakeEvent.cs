using UnityEngine;

namespace ProjectWendigo
{
    public class EarthquakeEvent : MonoBehaviour
    {
        [SerializeField] private GameObject _exitRocks;

        private void Awake()
        {
            Singletons.Main.Event.On("EarthquakeEvent", this.StartEvent);
        }

        private void StartEvent()
        {
            LevelMineStateContext.Instance.EnterEarthquakeEvent();
            Singletons.Main.Player.PlayerBody.GetComponent<IntervalSoundEmitter>().Play();
            this._exitRocks.SetActive(false);
        }
    }
}
