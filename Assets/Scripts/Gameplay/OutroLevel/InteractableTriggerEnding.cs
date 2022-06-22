using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectWendigo
{
    public class InteractableTriggerEnding : AInteractable
    {
        public enum Characters {Daughter, Samodiva};
        public Characters character;
        public ParticleSystem screenParticles;
        public GameObject focusObject;
        public float minBlendTime = 7f;
        public float maxBlendTime = 10f;

        // public GameObject blackOutSquare;
        // public float fadeSpeed = 15f;
        // private float fadeAmount;
        // private Color objectColor;

        public ParticleSystem portal;

        public ParticleSystem portal;

        void Start()
        {
            screenParticles.Stop();
        }

        public override void OnLookAt(GameObject target)
        {
            if(character == Characters.Daughter)
            {
                var options = new MessagePanelOptions
                {
                    Text = $"- Press {Singletons.Main.Input.GetBinding("Player/Interact")} to save daughter -",
                    Location = MessagePanelLocation.BottomCenter
                };
                Singletons.Main.Interface.OpenMessagePanel(options);

            }else {
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
            StartCoroutine(FadeBlackOutSquare());

            screenParticles.Play();
            ChangePortal();
            Singletons.Main.Camera.LockCamera();
            Singletons.Main.Camera.FocusOnTarget(focusObject, minBlendTime, maxBlendTime);


        }

        private void ChangePortal()
        {
            var size = portal.sizeOverLifetime;
            size.enabled = true;
            AnimationCurve curve = new AnimationCurve();
            curve.AddKey(0.0f, 0.0f);
            curve.AddKey(1.0f, 1.0f);
            size.size = new ParticleSystem.MinMaxCurve(1.5f, curve);
        }

        public IEnumerator FadeBlackOutSquare(bool fadeToBlack = true)
        {
            yield return new WaitForSecondsRealtime(5);
            Singletons.Main.Fade.FadeOutEffect();
        }
    }
}