using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEditor;
using System.Runtime.Serialization;

[CreateAssetMenu(fileName = "New inventory", menuName = "Inventory System/Inventory")]
public class InventoryObject : ScriptableObject {

    public string savePath;
    public ItemDatabaseObject database;
    public Inventory Container;
    

    public void AddItem(Item _item, int _amount) {
        for (int i = 0; i < Container.Items.Length; i++) {
            if (Container.Items[i].ID == _item.Id) {
                Container.Items[i].AddAmount(_amount);
                return;
            }
        }
        SetEmptySlot(_item, _amount);
    }

    public void SwitchItem(InventorySlot itemNew, InventorySlot itemOld) {
        InventorySlot temp = new InventorySlot(itemOld.ID, itemOld.item, itemOld.amount);
        itemOld.UpdateSlot(itemNew.ID, itemNew.item, itemNew.amount);
        itemNew.UpdateSlot(temp.ID, temp.item, temp.amount);
    }

    public void RemoveItem(Item _item) {
        for (int i = 0; i < Container.Items.Length; i++) {
            if (Container.Items[i].item == _item) {
                //-1 = value for empty slot
                Container.Items[i].UpdateSlot(-1, null, 0);
            }
        }
    }

    //Find first empty ionventoryslot
    public InventorySlot SetEmptySlot(Item _item, int _amount) {
        for (int i = 0; i < Container.Items.Length; i++) {
            if (Container.Items[i].ID <= -1) {
                Container.Items[i].UpdateSlot(_item.Id, _item, _amount);
                return Container.Items[i];
            }
        }
        //What happens when inventory is full
        return null;
    }

    //Save inventory to JSON file
    [ContextMenu("Save")]
    public void Save() {
        IFormatter formatter = new BinaryFormatter();
        Stream stream = new FileStream(string.Concat(Application.persistentDataPath, savePath), FileMode.Create, FileAccess.Write);
        formatter.Serialize(stream, Container);
        stream.Close();
    }

    //Load inventory from JSON file
    [ContextMenu("Load")]
    public void Load() {
        if (File.Exists(string.Concat(Application.persistentDataPath, savePath))) {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(string.Concat(Application.persistentDataPath, savePath), FileMode.Open, FileAccess.Read);
            Inventory newContainer = (Inventory)formatter.Deserialize(stream);
            
            for (int i = 0; i < Container.Items.Length; i++) {
                Container.Items[i].UpdateSlot(newContainer.Items[i].ID, newContainer.Items[i].item, newContainer.Items[i].amount);
            }
            stream.Close();
        }
    }

    [ContextMenu("Clear")]
    public void Clear() {
        for (int i = 0; i < Container.Items.Length; i++) {
                Container.Items[i].UpdateSlot(-1, null, 0);
        }
    }
}

[System.Serializable]
public class Inventory {
    public InventorySlot[] Items = new InventorySlot[35];
}

[System.Serializable]
public class InventorySlot {
    public int ID = -1;
    public Item item;
    public int amount;

    public InventorySlot() {
        ID = -1;
        item = null;
        amount = 0;
    }
    public InventorySlot(int _id, Item _item, int _amount) {
        ID = _id;
        item = _item;
        amount = _amount;
    }

    public void UpdateSlot(int _id, Item _item, int _amount) {
        ID = _id;
        item = _item;
        amount = _amount;
    }

    public void AddAmount(int value) {
        amount += value;
    }
}