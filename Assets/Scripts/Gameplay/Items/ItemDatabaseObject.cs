using System.Collections.Generic;
using UnityEngine;

namespace ProjectWendigo
{
    [CreateAssetMenu(fileName = "New item database", menuName = "Inventory System/Items/Database")]
    public class ItemDatabaseObject : ScriptableObject, ISerializationCallbackReceiver
    {
        public ItemObject[] Items;
        public Dictionary<int, ItemObject> GetItem { get; private set; } = new Dictionary<int, ItemObject>();

        //Save items to file
        public void OnAfterDeserialize()
        {
            for (int i = 0; i < Items.Length; i++)
            {
                this.Items[i].Id = i;
                this.GetItem.Add(i, Items[i]);
            }
        }

        public void OnBeforeSerialize()
        {
            this.GetItem = new Dictionary<int, ItemObject>();
        }
    }
}
