using UnityEngine;

public class BroadcastEvent : MonoBehaviour
{
    public void OnLightEnter(Collider collider)
    {
        collider.gameObject.BroadcastMessage("OnLightEnter", collider, SendMessageOptions.DontRequireReceiver);
    }

    public void OnLightExit(Collider collider)
    {
        collider.gameObject.BroadcastMessage("OnLightExit", collider, SendMessageOptions.DontRequireReceiver);
    }
}
