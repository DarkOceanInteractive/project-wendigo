using UnityEngine;

namespace ProjectWendigo.LevelMineStates
{
    public class Earthquake : AState<LevelMineStateContext>
    {
        public override void Enter()
        {
            SoundsSpawner soundsSpawner = Singletons.Main.Player.PlayerBody.GetComponent<SoundsSpawner>();
            DarkenCamera darkenCameraScript = Singletons.Main.Player.PlayerBody.GetComponent<DarkenCamera>();

            // Activate the earthquake sound ambience
            soundsSpawner.ActivateAmbience("Earthquake");
            ref SoundsSpawner.SoundAmbience pickaxeAmbience = ref soundsSpawner.FindAmbience("PickaxeHits");
            pickaxeAmbience.Range = new Vector2(10, 20);
            Singletons.Main.Sound.Play("samodiva_screaming");
        }
    }
}
