using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectWendigo
{
    public class ToggleInventory : MonoBehaviour
    {
        [SerializeField] public GameObject InventoryGameObject;
        [SerializeField] public bool IsCursorVisibleInInventory = true;

        protected void Start()
        {
            this.InventoryGameObject.SetActive(false);
        }

        protected void Update()
        {
            this.Inventory();
        }

        public void Inventory()
        {
            if (Singletons.Main.Input.PlayerToggledInventory)
            {
                if (!this.InventoryGameObject.activeSelf)
                {
                    this.InventoryGameObject.SetActive(true);
                    this.ShowMouseCursor();
                }
                else
                {
                    this.InventoryGameObject.SetActive(false);
                    this.HideMouseCursor();
                }
            }
        }

        public void ShowMouseCursor()
        {
            if (this.IsCursorVisibleInInventory)
                Singletons.Main.Input.ShowCursor();
        }

        public void HideMouseCursor()
        {
            if (this.IsCursorVisibleInInventory)
                Singletons.Main.Input.HideCursor();
        }
    }
}
