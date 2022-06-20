using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectWendigo
{
    public class InterfaceManager : MonoBehaviour
    {
        public enum Location
        {
            BottomCenter,
            TopRight
        }

        [System.Serializable]
        class MessagePanel
        {
            public GameObject GameObject;
            public Location Location;
            public string OnOpenSoundName;
            public string OnCloseSoundName;
            [HideInInspector] public float CountDown = 0f;

            public IEnumerator WaitForCountDown(float seconds, Action callback)
            {
                if (this.CountDown > 0f)
                {
                    // If a countdown is already active, just reset the timer and wait for it to finish
                    this.CountDown = seconds;
                    yield return new WaitUntil(() => this.CountDown <= 0f);
                }
                else
                {
                    this.CountDown = seconds;
                    while (this.CountDown > 0f)
                    {
                        this.CountDown -= Time.deltaTime;
                        yield return new WaitForEndOfFrame();
                    }
                    this.CountDown = 0f;
                }
                callback();
            }
        }

        [SerializeField] private MessagePanel[] _messagePanels;

        private MessagePanel GetMessagePanel(Location location)
        {
            return this._messagePanels.First(mp => mp.Location == location);
        }

        public void OpenMessagePanel(string text = null, Location location = Location.BottomCenter)
        {
            MessagePanel mp = this.GetMessagePanel(location);
            if (mp == null)
                return;
            if (text != null)
                mp.GameObject.GetComponentInChildren<Text>().text = text;
            if (mp.OnOpenSoundName != "")
                Singletons.Main.Sound.Play(mp.OnOpenSoundName);
            mp.GameObject.SetActive(true);
        }

        public void OpenMessagePanelForSeconds(float seconds, string text = null, Location location = Location.BottomCenter)
        {
            MessagePanel mp = this.GetMessagePanel(location);
            if (mp == null)
                return;
            this.OpenMessagePanel(text, location);
            StartCoroutine(mp.WaitForCountDown(seconds, () => this.CloseMessagePanel(location)));
        }

        public void CloseMessagePanel(Location location = Location.BottomCenter)
        {
            MessagePanel mp = this.GetMessagePanel(location);
            if (mp == null)
                return;
            if (mp.OnCloseSoundName != "")
                Singletons.Main.Sound.Play(mp.OnCloseSoundName);
            mp.GameObject.SetActive(false);
        }
    }
}
