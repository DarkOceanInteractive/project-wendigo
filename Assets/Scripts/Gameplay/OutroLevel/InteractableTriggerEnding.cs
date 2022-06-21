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

        public GameObject blackOutSquare;
        public float fadeSpeed = 15f;
        private float fadeAmount;
        private Color objectColor;

        void Start()
        {
            screenParticles.Stop();
        }

        public override void OnLookAt(GameObject target)
        {
            if(character == Characters.Daughter)
            {
                Singletons.Main.Interface.OpenMessagePanel("- Press F to save daughter -");
            }else {
                Singletons.Main.Interface.OpenMessagePanel("- Press F to attack Samodiva -");
            }
        }

        public override void OnInteract(GameObject target)
        {
            screenParticles.Play();
            Singletons.Main.Camera.LockCamera();
            Singletons.Main.Camera.FocusOnTarget(focusObject, minBlendTime, maxBlendTime);
            StartCoroutine(FadeBlackOutSquare());

        }

        public IEnumerator FadeBlackOutSquare(bool fadeToBlack = true)
        {
            objectColor = blackOutSquare.GetComponent<Image>().color;

            if (fadeToBlack)
            {
                while (objectColor.a < 1)
                {
                    fadeAmount = objectColor.a + (fadeSpeed * Time.deltaTime);

                    objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                    blackOutSquare.GetComponent<Image>().color = objectColor;
                    yield return null;
                }
            }
        }
    }
}