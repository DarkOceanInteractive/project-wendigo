using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectWendigo
{
    public class FocusCameraObject : MonoBehaviour
    {
        public GameObject target;

        public void FocusCamera()
        {
            Singletons.Main.Camera.FocusOnTarget(target);
        }
    }
}