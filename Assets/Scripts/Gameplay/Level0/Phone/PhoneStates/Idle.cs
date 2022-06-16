using UnityEngine;
using System.Collections.Generic;

namespace ProjectWendigo.PhoneStates
{
    public class Idle : AState<PhoneStateContext>
    {
        private static bool _hasRang = false;

        override public void Update()
        {
            if (!_hasRang)
            {
                this.context.Invoke("GoToRing", this.context.RingDelay);
                Idle._hasRang = true;
            }
        }
    }
}
