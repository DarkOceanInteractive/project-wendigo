using UnityEngine;

namespace ProjectWendigo.LightingStates
{
    public class Off : AState<LightingStateContext>
    {
        public override void Update()
        {
            this.context.GetComponent<Light>().enabled =  false;

            if (Singletons.Main.Input.LightingToggled)
            {
                this.context.SetState(new On());
            }
        }
    }
}
