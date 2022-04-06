using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment Object", menuName = "Inventory System/Items/Equipment")]
public class EquipmentObject : ItemObject
{
    public float amounts; //used for Ammunition or Battery amounts
    public void Awake() {
        type = ItemType.Equipment;
    }
}
