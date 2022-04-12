using UnityEngine;

namespace ProjectWendigo.LightingStates
{
    public class On : AState<LightingStateContext>
    {
        public override void Update()
        {
            this.context.GetComponent<Light>().enabled =  true;

            if (Singletons.Main.Input.LightingToggled)
            {
                this.context.SetState(new Off());
            }
        }
    }
}
