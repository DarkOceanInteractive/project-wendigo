using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Runtime.Serialization;

namespace ProjectWendigo
{
    [CreateAssetMenu(fileName = "New inventory", menuName = "Inventory System/Inventory")]
    public class InventoryObject : ScriptableObject
    {
        public string SavePath;
        public ItemDatabaseObject Database;
        public Inventory Container;

        public void AddItem(Item item, int amount)
        {
            for (int i = 0; i < this.Container.Items.Length; i++)
            {
                if (this.Container.Items[i].Id == item.Id)
                {
                    this.Container.Items[i].AddAmount(amount);
                    return;
                }
            }
            this.SetEmptySlot(item, amount);
        }

        public void SwitchItem(InventorySlot newItem, InventorySlot oldItem)
        {
            InventorySlot temp = new InventorySlot(oldItem.Id, oldItem.Item, oldItem.Amount);
            oldItem.UpdateSlot(newItem.Id, newItem.Item, newItem.Amount);
            newItem.UpdateSlot(temp.Id, temp.Item, temp.Amount);
        }

        public void RemoveItem(Item item)
        {
            for (int i = 0; i < this.Container.Items.Length; i++)
            {
                if (this.Container.Items[i].Item == item)
                {
                    this.Container.Items[i].Reset();
                }
            }
        }

        //Find first empty ionventoryslot
        public InventorySlot SetEmptySlot(Item item, int amount)
        {
            for (int i = 0; i < this.Container.Items.Length; i++)
            {
                if (this.Container.Items[i].Id <= -1)
                {
                    this.Container.Items[i].UpdateSlot(item.Id, item, amount);
                    return this.Container.Items[i];
                }
            }
            //What happens when inventory is full
            return null;
        }

        //Save inventory to JSON file
        [ContextMenu("Save")]
        public void Save()
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(string.Concat(Application.persistentDataPath, this.SavePath), FileMode.Create, FileAccess.Write);
            formatter.Serialize(stream, this.Container);
            stream.Close();
        }

        //Load inventory from JSON file
        [ContextMenu("Load")]
        public void Load()
        {
            if (File.Exists(string.Concat(Application.persistentDataPath, this.SavePath)))
            {
                IFormatter formatter = new BinaryFormatter();
                Stream stream = new FileStream(string.Concat(Application.persistentDataPath, this.SavePath), FileMode.Open, FileAccess.Read);
                Inventory newContainer = formatter.Deserialize(stream) as Inventory;

                for (int i = 0; i < this.Container.Items.Length; i++)
                {
                    this.Container.Items[i].UpdateSlot(newContainer.Items[i].Id, newContainer.Items[i].Item, newContainer.Items[i].Amount);
                }
                stream.Close();
            }
        }

        [ContextMenu("Clear")]
        public void Clear()
        {
            for (int i = 0; i < this.Container.Items.Length; i++)
            {
                this.Container.Items[i].Reset();
            }
        }
    }
}
