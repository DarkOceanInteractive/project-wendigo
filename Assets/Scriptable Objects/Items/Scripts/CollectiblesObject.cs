using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Collectables Object", menuName = "Inventory System/Items/Collectables")]
public class CollectiblesObject : ItemObject
{
   public void Awake() {
       type = ItemType.Collectables;
   }
}
