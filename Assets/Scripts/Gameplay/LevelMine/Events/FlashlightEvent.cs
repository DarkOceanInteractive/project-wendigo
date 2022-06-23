using UnityEngine;
using System.Collections;

namespace ProjectWendigo
{
    public class FlashlightEvent : MonoBehaviour
    {
        [SerializeField] private LightingStateContext _flashlightContext;
        [SerializeField] private GameObject _flashlightGameObject;
        [SerializeField] private Animator _flashlightAnimator;
        [SerializeField] private string _flashlightDropTrigger;
        [SerializeField] private float _drainDelay = 1.0f;
        [SerializeField] private string _journalEntryTitle;

        [SerializeField] private string _ambientSoundEffectName;
        [SerializeField] private float _ambientSoundEffectVolume = 0.2f;

        private void Awake()
        {
            Singletons.Main.Event.On("FlashlightEvent", this.EnterFlashlightEvent);
        }

        private void Start()
        {
            if (this._ambientSoundEffectName != "")
                {
                    AudioSource audio = Singletons.Main.Sound.GetAudio(this._ambientSoundEffectName);
                    audio.volume *= this._ambientSoundEffectVolume;
                    audio.Play();
                }
            Singletons.Main.Event.Trigger("FlashlightEvent");
        }

        private void EnterFlashlightEvent()
        {
            StartCoroutine(this.FlashlightEventActions());
        }

        private IEnumerator FlashlightEventActions()
        {
            yield return this.DrainBattery();
            Singletons.Main.Notebook.AddJournalEntryByTitle(this._journalEntryTitle);
        }

        public IEnumerator DrainBattery()
        {
            this._flashlightContext.SetState(new LightingStates.Flickering());
            while (this._flashlightContext.Battery > 0)
            {
                this._flashlightContext.Battery -= 0.1f;
                yield return new WaitForSeconds(this._drainDelay);
            }            
            this._flashlightContext.SetState(new LightingStates.Off());
            this._flashlightAnimator.SetTrigger(this._flashlightDropTrigger);
            yield return new WaitForEndOfFrame();
            yield return new WaitForSeconds(this._flashlightAnimator.GetCurrentAnimatorClipInfo(0).Length);
            this._flashlightGameObject.SetActive(false);
        }
    }
}
