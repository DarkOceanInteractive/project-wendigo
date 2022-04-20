using UnityEngine;

public class BroadcastEvent : MonoBehaviour
{
    public void OnLightEnter(Collider collider)
    {
        collider.gameObject.BroadcastMessage("OnLightEnter", null, SendMessageOptions.DontRequireReceiver);
    }

    public void OnLightExit(Collider collider)
    {
        collider.gameObject.BroadcastMessage("OnLightExit", null, SendMessageOptions.DontRequireReceiver);
    }
}
