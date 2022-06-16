using UnityEngine;

namespace ProjectWendigo.PhoneStates
{
    public class Ringing : AState<PhoneStateContext>
    {
        [SerializeField] private string _ringingSoundName = "cabin_phone_ringing";
        private AudioSource _ringingAudioSource;

        override public void Enter()
        {
            this._ringingAudioSource = Singletons.Main.Sound.GetAudioAt(this._ringingSoundName, this.context.transform.position);
            this._ringingAudioSource.Play();
        }

        public override void Exit()
        {
            this._ringingAudioSource.Stop();
        }
    }
}
