using UnityEngine;

namespace ProjectWendigo
{
    public class LevelMineStateContext : AStateContext
    {
        private static LevelMineStateContext _instance;
        private static GameObject _handle;

        static public LevelMineStateContext Instance
        {
            get
            {
                if (LevelMineStateContext._instance == null)
                {
                    LevelMineStateContext._handle = new GameObject();
                    LevelMineStateContext._instance = LevelMineStateContext._handle.AddComponent<LevelMineStateContext>();
                }
                return LevelMineStateContext._instance;
            }
        }

        public LevelMineStateContext()
        {
            this.SetState(new LevelMineStates.Default());
        }

        public void EnterEarthquakeEvent()
        {
            if (!this.IsInState<LevelMineStates.Earthquake>())
                this.SetState(new LevelMineStates.Earthquake());
        }

        public void EnterLanternEvent()
        {
            if (!this.IsInState<LevelMineStates.Lantern>())
                this.SetState(new LevelMineStates.Lantern());
        }
    }
}
