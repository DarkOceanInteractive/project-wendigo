using UnityEngine;

namespace ProjectWendigo
{
    [CreateAssetMenu(fileName = "New Equipment Object", menuName = "Inventory System/Items/Equipment")]
    public class EquipmentObject : ItemObject
    {
        public EquipmentObject()
        {
            this.Type = ItemType.Equipment;
        }
    }
}
