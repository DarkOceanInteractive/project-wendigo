using System.Collections;
using UnityEngine;

namespace ProjectWendigo
{
    public class InteractableTriggerEnding : AInteractable
    {
        public enum Characters { Daughter, Samodiva };
        public Characters Character;
        public ParticleSystem ScreenParticles;
        public GameObject FocusObject;
        public float MinBlendTime = 7f;
        public float MaxBlendTime = 10f;
        public ParticleSystem Portal;
        public GameObject Credits;
        public float CreditsScrollSpeed = 100f;

        void Start()
        {
            this.ScreenParticles.Stop();
        }

        public override void OnLookAt(GameObject target)
        {
            if (this.Character == Characters.Daughter)
            {
                var options = new MessagePanelOptions
                {
                    Text = $"- Press {Singletons.Main.Input.GetBinding("Player/Interact")} to save your daughter -",
                    Location = MessagePanelLocation.BottomCenter
                };
                Singletons.Main.Interface.OpenMessagePanel(options);
            }
            else
            {
                var options = new MessagePanelOptions
                {
                    Text = $"- Press {Singletons.Main.Input.GetBinding("Player/Interact")} to attack Samodiva -",
                    Location = MessagePanelLocation.BottomCenter
                };
                Singletons.Main.Interface.OpenMessagePanel(options);
            }
        }

        public override void OnInteract(GameObject target)
        {
            StartCoroutine(this.FadeBlackOutSquare());

            this.ScreenParticles.Play();
            this.ChangePortal();
            Singletons.Main.Camera.LockCamera();
            Singletons.Main.Camera.FocusOnTarget(this.FocusObject, this.MinBlendTime, this.MaxBlendTime);
        }

        private void ChangePortal()
        {
            var size = Portal.sizeOverLifetime;
            size.enabled = true;
            AnimationCurve curve = new AnimationCurve();
            curve.AddKey(0.0f, 0.0f);
            curve.AddKey(1.0f, 1.0f);
            size.size = new ParticleSystem.MinMaxCurve(1.5f, curve);
        }

        public IEnumerator FadeBlackOutSquare()
        {
            yield return new WaitForSecondsRealtime(5);
            Singletons.Main.Fade.FadeOutEffect();
            Invoke(nameof(this.StartCredits), 4f);
        }

        private void StartCredits()
        {
            this.Credits.SetActive(true);
            Invoke(nameof(this.StartCreditsScrolling), 5f);
        }

        private IEnumerator ScrollCredits()
        {
            while (true)
            {
                this.Credits.transform.Translate(Vector3.up * Time.deltaTime * this.CreditsScrollSpeed);
                yield return new WaitForEndOfFrame();
            }
        }

        private void StartCreditsScrolling()
        {
            StartCoroutine(this.ScrollCredits());
            Invoke(nameof(this.EndGame), 30f);
        }

        private void EndGame()
        {
            Application.Quit();
        }
    }
}
