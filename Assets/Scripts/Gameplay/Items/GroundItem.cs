using UnityEngine;

namespace ProjectWendigo
{
    public class GroundItem : MonoBehaviour
    {
        public ItemObject Item;

        public virtual void OnGrab()
        {
            Destroy(this.gameObject);
        }
    }
}
