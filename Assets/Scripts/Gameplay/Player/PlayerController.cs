using UnityEngine;

namespace ProjectWendigo
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private CharacterController _characterController;
        private Vector3 _motion = Vector3.zero;
        public InventoryObject inventory;
        //public ItemPickup itemPickup;

        protected void OnApplicationQuit()
        {
            inventory.Container.Items = new InventorySlot[35];
        }

        protected void OnTriggerEnter(Collider other)
        {
            // Makes player add item to inventory when walking over
            GroundItem groundItem = other.GetComponent<GroundItem>();
            if (groundItem)
            {
                Item inventoryItem = new Item(groundItem.Item);
                Debug.Log($"Grabbed {inventoryItem.Name} {inventoryItem.Id}");
                inventory.AddItem(inventoryItem, 1);
                groundItem.OnGrab();
            }
        }

        protected void Update()
        {
            //Save Inventory by pressing R
            if (Singletons.Main.Input.PlayerSavedInventory)
            {
                inventory.Save();
            }
            //Load Inventory by pressing T
            if (Singletons.Main.Input.PlayerLoadedInventory)
            {
                inventory.Load();
            }
            //Grab Item by pressing F
            if (Singletons.Main.Input.PlayerGrabbedItem)
            {
                //Not implemented
            }

            if (this._motion != Vector3.zero)
            {
                _ = this._characterController.Move(this._motion);
                this._motion = Vector3.zero;
            }
        }

        /// <summary>
        /// Move the character controller by `motion`.
        /// </summary>
        /// <param name="motion">Motion to apply</param>
        public void Move(Vector3 motion)
        {
            this._motion += motion;
        }
    }
}
