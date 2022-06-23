using UnityEngine;

namespace ProjectWendigo
{
    public class SamodivaEvent : MonoBehaviour
    {
        [SerializeField] private GameObject _samodiva;
        [SerializeField] private GameObject _focusAt;

        public void StartSamodivaEvent()
        {
            Singletons.Main.Player.PlayerBody.GetComponent<SoundsSpawner>().DeactivateAmbience("Earthquake");
            Singletons.Main.Player.PlayerBody.GetComponent<SoundsSpawner>().DeactivateAmbience("PickaxeHits");
            this._samodiva.GetComponent<SoundEmitter>().Play();
            this.Invoke(nameof(this.TurnAround), .5f);
            this.Invoke(nameof(this.MakeSamodivaAppear), 2f);
            this.Invoke(nameof(this.FadeToBlack), 3.2f);
            this.Invoke(nameof(this.GoToNextScene), 7f);
        }

        private void TurnAround()
        {
            Singletons.Main.Camera.FocusOnTarget(this._focusAt, .1f, .7f);
        }

        private void MakeSamodivaAppear()
        {
            this._samodiva.GetComponent<FollowRoute>().Go();
        }

        private void FadeToBlack()
        {
            Singletons.Main.Fade.FadeOutEffect();
        }

        private void GoToNextScene()
        {
            Singletons.Main.Scene.GoToNextScene();
        }
    }
}
