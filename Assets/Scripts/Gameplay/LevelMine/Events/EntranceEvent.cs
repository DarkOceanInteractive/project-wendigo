using UnityEngine;

namespace ProjectWendigo
{
    public class EntranceEvent : MonoBehaviour
    {
        private bool _hasVisitedEntrance = false;
        private SoundsSpawner _soundsSpawner;

        private void Awake()
        {
            this._soundsSpawner = Singletons.Main.Player.PlayerBody.GetComponent<SoundsSpawner>();
        }

        public void OnEnterEntrance()
        {
            if (!this._hasVisitedEntrance)
            {
                this._hasVisitedEntrance = true;
                this._soundsSpawner.ActivateAmbience("Entrance");
            }
        }

        public void OnExitEntrance()
        {
            this._soundsSpawner.DeactivateAmbience("Entrance");
        }
    }
}
