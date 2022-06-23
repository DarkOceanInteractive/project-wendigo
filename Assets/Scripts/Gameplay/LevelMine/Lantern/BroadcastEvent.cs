using UnityEngine;

namespace ProjectWendigo
{
    public class BroadcastEvent : MonoBehaviour
    {
        public void OnLightEnter(Collider collider)
        {
            if (!LevelMineStateContext.Instance.IsInState<LevelMineStates.Earthquake>())
                collider.gameObject.BroadcastMessage("OnLightEnter", SendMessageOptions.DontRequireReceiver);
        }

        public void OnLightExit(Collider collider)
        {
            if (!LevelMineStateContext.Instance.IsInState<LevelMineStates.Earthquake>())
                collider.gameObject.BroadcastMessage("OnLightExit", SendMessageOptions.DontRequireReceiver);
        }
    }
}
