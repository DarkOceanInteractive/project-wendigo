using UnityEngine;

namespace ProjectWendigo
{
    [CreateAssetMenu(fileName = "New Default Object", menuName = "Inventory System/Items/Default")]
    public class DefaultObject : ItemObject
    {
        public DefaultObject()
        {
            this.Type = ItemType.Default;
        }
    }
}
