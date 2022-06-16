using UnityEngine;

namespace ProjectWendigo.PhoneStates
{
    public class Calling : AState<PhoneStateContext>
    {
        [SerializeField] private string _callSoundName = "cabin_phone_static";

        override public void Enter()
        {
            Singletons.Main.Sound.PlayAt(this._callSoundName, this.context.transform.position);
        }
    }
}
