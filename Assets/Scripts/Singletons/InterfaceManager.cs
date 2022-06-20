using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectWendigo
{
    public enum MessagePanelLocation
    {
        BottomCenter,
        TopRight
    }

    public class MessagePanelOptions
    {
        public float Timeout = 0f;
        public Func<bool> Until = null;
        public string Text = null;
        public MessagePanelLocation Location = MessagePanelLocation.BottomCenter;
    }

    public class InterfaceManager : MonoBehaviour
    {
        [System.Serializable]
        class MessagePanel
        {
            public GameObject GameObject;
            public MessagePanelLocation Location;
            public string OnOpenSoundName;
            public string OnCloseSoundName;
            private float _countDown = 0f;
            private bool _isOpen = false;

            public void Open()
            {
                this.GameObject.SetActive(true);
                this._isOpen = true;
                if (this.OnOpenSoundName != "")
                    Singletons.Main.Sound.Play(this.OnOpenSoundName);
            }

            public void Close()
            {
                this.GameObject.SetActive(false);
                this._isOpen = false;
                if (this.OnCloseSoundName != "")
                    Singletons.Main.Sound.Play(this.OnCloseSoundName);
            }

            public IEnumerator WaitForCountDown(float seconds, Action callback, Func<bool> orUntil = null)
            {
                if (orUntil == null)
                    orUntil = () => false;
                if (this._countDown > 0f)
                {
                    // If a countdown is already active, just reset the timer and wait for it to finish
                    this._countDown = seconds;
                    yield return new WaitUntil(() => this._countDown <= 0f || orUntil());
                }
                else
                {
                    this._countDown = seconds;
                    while (this._countDown > 0f && this._isOpen && !orUntil())
                    {
                        this._countDown -= Time.deltaTime;
                        yield return new WaitForEndOfFrame();
                    }
                    this._countDown = 0f;
                }
                callback();
            }

            public IEnumerator WaitUntil(Func<bool> until, Action callback)
            {
                yield return new WaitUntil(() => !this._isOpen || until());
                callback();
            }
        }

        [SerializeField] private MessagePanel[] _messagePanels;

        private MessagePanel GetMessagePanel(MessagePanelLocation location)
        {
            return this._messagePanels.First(mp => mp.Location == location);
        }

        public void OpenMessagePanel(MessagePanelOptions options = default)
        {
            MessagePanel mp = this.GetMessagePanel(options.Location);
            if (mp == null)
                return;
            if (options.Text != null)
                mp.GameObject.GetComponentInChildren<Text>().text = options.Text;
            mp.Open();
            if (options.Timeout != 0f)
                StartCoroutine(mp.WaitForCountDown(
                    options.Timeout,
                    () => this.CloseMessagePanel(options.Location),
                    options.Until));
            else if (options.Until != null)
                StartCoroutine(mp.WaitUntil(
                    options.Until,
                    () => this.CloseMessagePanel(options.Location)));
        }

        public void CloseMessagePanel(MessagePanelLocation location = MessagePanelLocation.BottomCenter)
        {
            MessagePanel mp = this.GetMessagePanel(location);
            if (mp == null)
                return;
            mp.Close();
        }
    }
}
