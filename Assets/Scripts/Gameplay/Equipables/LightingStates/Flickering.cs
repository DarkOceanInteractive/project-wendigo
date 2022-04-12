using UnityEngine;
using System.Collections;

namespace ProjectWendigo.LightingStates
{
    public class Flickering : AState<LightingStateContext>
    {
        private float _timer;

        public override void Enter()
        {
            GenerateTimer();
        }

        public override void Update()
        {
            if (_timer > 0)
            {
                _timer -= Time.deltaTime;
            }
            else
            {
                this.context.GetComponent<Light>().enabled = !this.context.GetComponent<Light>().enabled;
                GenerateTimer();
            }
        }

        private void GenerateTimer()
        {
            _timer = Random.Range(0.01f, Mathf.Abs(this.context.Battery - System.Convert.ToInt32(!this.context.GetComponent<Light>().enabled)));
        }
    }
}
