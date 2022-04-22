using UnityEngine;

namespace ProjectWendigo.LevelMineStates
{
    public class Earthquake : AState<LevelMineStateContext>
    {
        public override void Enter()
        {
            GameObject playerBody = GameObject.Find("Player Body");
            SoundsSpawner soundsSpawner = playerBody.GetComponent<SoundsSpawner>();
            DarkenCamera darkenCameraScript = playerBody.GetComponent<DarkenCamera>();

            // Disable the darkness effect
            Object.Destroy(darkenCameraScript);
            // Deactivate all sounds ambiences
            soundsSpawner.DeactivateAmbience("Light");
            soundsSpawner.DeactivateAmbience("Dark");
            soundsSpawner.DeactivateAmbience("General");
            // Activate the earthquake sound ambience
            soundsSpawner.ActivateAmbience("Earthquake");
            ref SoundsSpawner.SoundAmbience pickaxeAmbience = ref soundsSpawner.FindAmbience("PickaxeHits");
            pickaxeAmbience.Range = new Vector2(10, 20);
            Singletons.Main.Sound.Play("samodiva_screaming");
        }
    }
}
