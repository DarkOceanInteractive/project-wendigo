using UnityEngine;

namespace ProjectWendigo.PhoneStates
{
    public class Calling : AState<PhoneStateContext>
    {
        [SerializeField] private string _callStaticSoundName = "cabin_phone_static";
        [SerializeField] private string _callVoiceSoundName = "cabin_phone_voice";

        override public void Enter()
        {
            Singletons.Main.Sound.PlayAt(this._callStaticSoundName, this.context.transform.position);
            Singletons.Main.Sound.PlayAt(this._callVoiceSoundName, this.context.transform.position);
        }
    }
}
