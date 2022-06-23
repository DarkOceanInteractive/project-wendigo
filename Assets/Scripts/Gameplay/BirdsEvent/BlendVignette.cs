using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectWendigo
{
    public class BlendVignette : MonoBehaviour
    {
        public void StartVignette(float time)
        {
            Singletons.Main.Camera.BlendVignette(0.4f, time);
        }

        public void StopVignette(float time)
        {
            Singletons.Main.Camera.BlendVignette(0f, time);
        }
    }
}

