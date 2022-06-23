using UnityEngine;

namespace ProjectWendigo
{
    public class LightingStateContext : AStateContext
    {
        [Range(0f, 1f)] public float Battery = 0.8f;

        // public void Start()
        // {
        //     this.SetState(new LightingStates.Off());
        // }

        protected override void Update()
        {
            base.Update();
        }
    }
}
