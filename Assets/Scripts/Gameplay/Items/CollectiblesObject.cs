using UnityEngine;

namespace ProjectWendigo
{
    [CreateAssetMenu(fileName = "New Collectables Object", menuName = "Inventory System/Items/Collectables")]
    public class CollectiblesObject : ItemObject
    {
        public CollectiblesObject()
        {
            this.Type = ItemType.Collectables;
        }
    }
}
