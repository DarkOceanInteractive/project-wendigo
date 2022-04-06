using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

namespace ProjectWendigo
{
    public class DisplayInventory : MonoBehaviour
    {
        public MouseItem mouseItem = new MouseItem();
        public GameObject inventoryPrefab;
        public InventoryObject inventory;
        public int X_START;
        public int Y_START;
        public int X_SPACE_BETWEEN_ITEMS;
        public int Y_SPACE_BETWEEN_ITEMS; 
        public int NUMBER_OF_COLUMNS;
        Dictionary<GameObject, InventorySlot> itemsDisplayed = new Dictionary<GameObject, InventorySlot>();

        void Start() {
            CreateSlots();
        }

        public void CreateSlots() {
            itemsDisplayed = new Dictionary<GameObject, InventorySlot>();
            for (int i = 0; i < inventory.Container.Items.Length; i++) {
                var obj = Instantiate(inventoryPrefab, Vector3.zero, Quaternion.identity, transform);
                obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
                //Add different events to item slot
                AddEvent(obj, EventTriggerType.PointerEnter, delegate { OnEnter(obj); });
                AddEvent(obj, EventTriggerType.PointerExit, delegate { OnExit(obj); });
                AddEvent(obj, EventTriggerType.BeginDrag, delegate { OnDragStart(obj); });
                AddEvent(obj, EventTriggerType.EndDrag, delegate { OnDragEnd(obj); });
                AddEvent(obj, EventTriggerType.Drag, delegate { OnDrag(obj); });
                itemsDisplayed.Add(obj, inventory.Container.Items[i]);
            }
        }
        
        void Update() {
            UpdateSlots();
        }

        public void UpdateSlots() {
            foreach (KeyValuePair<GameObject, InventorySlot> _slot in itemsDisplayed) {
                if (_slot.Value.ID >= 0) {
                    //Change item sprite
                    _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = inventory.database.GetItem[_slot.Value.item.Id].uiDisplay;
                    //Change background color
                    _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
                    //If only one item, don't show text
                    if (_slot.Value.amount == 1) {
                        _slot.Key.GetComponentInChildren<TextMeshProUGUI>().text = "";
                    } else {
                        _slot.Key.GetComponentInChildren<TextMeshProUGUI>().text = _slot.Value.amount.ToString("n0");
                    }
                }
                else {
                    //Set all variables to standard if slot should be empty
                    _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = null;
                    _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0);
                    _slot.Key.GetComponentInChildren<TextMeshProUGUI>().text = "";
                }
            }
        }

        public Vector3 GetPosition(int i) {
            return new Vector3(X_START + (X_SPACE_BETWEEN_ITEMS * (i % NUMBER_OF_COLUMNS)), Y_START + (-Y_SPACE_BETWEEN_ITEMS * (i / NUMBER_OF_COLUMNS)), 0f);
        }
        
        //Add different type of events to object
        private void AddEvent(GameObject obj, EventTriggerType type, UnityAction<BaseEventData> action) {
            EventTrigger trigger = obj.GetComponent<EventTrigger>();
            var eventTrigger = new EventTrigger.Entry();
            eventTrigger.eventID = type;
            eventTrigger.callback.AddListener(action);
            trigger.triggers.Add(eventTrigger);
        }

        //When cursor enters slot
        public void OnEnter(GameObject obj) {
            mouseItem.hoverObj = obj;
            if (itemsDisplayed.ContainsKey(obj))
                mouseItem.hoverItem = itemsDisplayed[obj];
        }

        //When cursor exits slot
        public void OnExit(GameObject obj) {
            mouseItem.hoverObj = null;
            mouseItem.hoverItem = null;
        }

        public void OnDrag(GameObject obj) {
            if (mouseItem.obj != null)
                mouseItem.obj.GetComponent<RectTransform>().position = Singletons.Main.Input.MousePosition;
        }

        //Tracks cursor and shows sprite on drag
        public void OnDragStart(GameObject obj) {
            var mouseObject = new GameObject();
            var rt = mouseObject.AddComponent<RectTransform>();
            rt.sizeDelta = new Vector2(50, 50);
            mouseObject.transform.SetParent(transform.parent);

            if (itemsDisplayed[obj].ID >= 0) {
                var img = mouseObject.AddComponent<Image>();
                img.sprite = inventory.database.GetItem[itemsDisplayed[obj].ID].uiDisplay;
                img.raycastTarget = false;
            }
            mouseItem.obj = mouseObject;
            mouseItem.item = itemsDisplayed[obj];
        }

        //Move or delete item from inventory when releasing cursor
        public void OnDragEnd(GameObject obj) {
            if (mouseItem.hoverObj) {
                inventory.SwitchItem(itemsDisplayed[obj], itemsDisplayed[mouseItem.hoverObj]);
            } else {
                inventory.RemoveItem(itemsDisplayed[obj].item);
            }
            Destroy(mouseItem.obj);
            mouseItem.item = null;
        }
    }

    public class MouseItem {
        public GameObject obj;
        public GameObject hoverObj;
        public InventorySlot item;
        public InventorySlot hoverItem;
    }
}