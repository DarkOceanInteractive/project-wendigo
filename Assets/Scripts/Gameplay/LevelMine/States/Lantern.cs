using UnityEngine;

namespace ProjectWendigo.LevelMineStates
{
    public class Lantern : AState<LevelMineStateContext>
    {
        public override void Enter()
        {
            DarkenCamera darkenCameraScript = Singletons.Main.Player.PlayerBody.GetComponent<DarkenCamera>();
            // Disable the darkness effect
            Object.Destroy(darkenCameraScript);
            SoundsSpawner soundsSpawner = Singletons.Main.Player.PlayerBody.GetComponent<SoundsSpawner>();
            // Deactivate all sounds ambiences
            soundsSpawner.DeactivateAmbience("Light");
            soundsSpawner.DeactivateAmbience("Dark");
            soundsSpawner.DeactivateAmbience("General");
        }
    }
}
