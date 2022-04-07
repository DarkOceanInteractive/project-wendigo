using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

namespace ProjectWendigo
{
    public struct MouseItem
    {
        public GameObject obj;
        public GameObject hoverObj;
        public InventorySlot item;
        public InventorySlot hoverItem;
    }

    public class DisplayInventory : MonoBehaviour
    {
        public MouseItem MouseItem = new MouseItem();
        public GameObject InventoryPrefab;
        public InventoryObject Inventory;
        public int StartX;
        public int StartY;
        public int XSpaceBetweenItems;
        public int YSpaceBetweenItems;
        public int NumberOfColumns;
        public Dictionary<GameObject, InventorySlot> ItemsDisplayed = new Dictionary<GameObject, InventorySlot>();

        protected void Start()
        {
            this.CreateSlots();
        }

        protected void Update()
        {
            this.UpdateSlots();
        }

        public void CreateSlots()
        {
            this.ItemsDisplayed = new Dictionary<GameObject, InventorySlot>();
            for (int i = 0; i < this.Inventory.Container.Items.Length; i++)
            {
                GameObject obj = Instantiate(this.InventoryPrefab, Vector3.zero, Quaternion.identity, this.transform);
                obj.GetComponent<RectTransform>().localPosition = this.GetPosition(i);
                //Add different events to item slot
                this.AddEvent(obj, EventTriggerType.PointerEnter, delegate { this.OnEnter(obj); });
                this.AddEvent(obj, EventTriggerType.PointerExit, delegate { this.OnExit(obj); });
                this.AddEvent(obj, EventTriggerType.BeginDrag, delegate { this.OnDragStart(obj); });
                this.AddEvent(obj, EventTriggerType.EndDrag, delegate { this.OnDragEnd(obj); });
                this.AddEvent(obj, EventTriggerType.Drag, delegate { this.OnDrag(obj); });
                this.ItemsDisplayed.Add(obj, this.Inventory.Container.Items[i]);
            }
        }

        public void UpdateSlots()
        {
            foreach (KeyValuePair<GameObject, InventorySlot> slot in this.ItemsDisplayed)
            {
                if (slot.Value.Id >= 0)
                {
                    //Change item sprite
                    slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = this.Inventory.Database.GetItem[slot.Value.Item.Id].UiDisplay;
                    //Change background color
                    slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
                    //If only one item, don't show text
                    if (slot.Value.Amount == 1)
                    {
                        slot.Key.GetComponentInChildren<TextMeshProUGUI>().text = "";
                    }
                    else
                    {
                        slot.Key.GetComponentInChildren<TextMeshProUGUI>().text = slot.Value.Amount.ToString("n0");
                    }
                }
                else
                {
                    //Set all variables to standard if slot should be empty
                    slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = null;
                    slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0);
                    slot.Key.GetComponentInChildren<TextMeshProUGUI>().text = "";
                }
            }
        }

        public Vector3 GetPosition(int i)
        {
            return new Vector3(
                this.StartX + (this.XSpaceBetweenItems * (i % this.NumberOfColumns)),
                this.StartY + (-this.YSpaceBetweenItems * (i / this.NumberOfColumns)),
                0f
            );
        }

        //Add different type of events to object
        private void AddEvent(GameObject obj, EventTriggerType type, UnityAction<BaseEventData> action)
        {
            EventTrigger trigger = obj.GetComponent<EventTrigger>();
            EventTrigger.Entry entry = new EventTrigger.Entry
            {
                eventID = type
            };
            entry.callback.AddListener(action);
            trigger.triggers.Add(entry);
        }

        //When cursor enters slot
        public void OnEnter(GameObject obj)
        {
            this.MouseItem.hoverObj = obj;
            if (this.ItemsDisplayed.ContainsKey(obj))
                this.MouseItem.hoverItem = this.ItemsDisplayed[obj];
        }

        //When cursor exits slot
        public void OnExit(GameObject obj)
        {
            this.MouseItem.hoverObj = null;
            this.MouseItem.hoverItem = null;
        }

        public void OnDrag(GameObject obj)
        {
            if (this.MouseItem.obj != null)
                this.MouseItem.obj.GetComponent<RectTransform>().position = Singletons.Main.Input.MousePosition;
        }

        //Tracks cursor and shows sprite on drag
        public void OnDragStart(GameObject obj)
        {
            GameObject mouseObject = new GameObject();
            RectTransform rt = mouseObject.AddComponent<RectTransform>();
            rt.sizeDelta = new Vector2(50, 50);
            mouseObject.transform.SetParent(this.transform.parent);

            if (ItemsDisplayed[obj].Id >= 0)
            {
                Image img = mouseObject.AddComponent<Image>();
                img.sprite = Inventory.Database.GetItem[this.ItemsDisplayed[obj].Id].UiDisplay;
                img.raycastTarget = false;
            }
            this.MouseItem.obj = mouseObject;
            this.MouseItem.item = this.ItemsDisplayed[obj];
        }

        //Move or delete item from inventory when releasing cursor
        public void OnDragEnd(GameObject obj)
        {
            if (this.MouseItem.hoverObj)
                this.Inventory.SwitchItem(ItemsDisplayed[obj], this.ItemsDisplayed[this.MouseItem.hoverObj]);
            else
                this.Inventory.RemoveItem(this.ItemsDisplayed[obj].Item);
            Destroy(this.MouseItem.obj);
            this.MouseItem.item = null;
        }
    }
}
