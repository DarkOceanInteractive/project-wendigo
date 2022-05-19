using UnityEngine;

namespace ProjectWendigo
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private CharacterController _characterController;
        private Vector3 _motion = Vector3.zero;
        public InventoryObject inventory;
        [SerializeField] private GameObject _lantern;
        [SerializeField] private GameObject _exitRocks;
        private bool _hasEquipped;

        protected void OnApplicationQuit()
        {
            inventory.Container.Items = new InventorySlot[35];
        }

        // protected void OnTriggerEnter(Collider other)
        // {
        //     // Makes player add item to inventory when walking over
        //     GroundItem groundItem = other.GetComponent<GroundItem>();
        //     if (groundItem)
        //     {
        //         Item inventoryItem = new Item(groundItem.Item);
        //         Debug.Log($"Grabbed {inventoryItem.Name} {inventoryItem.Id}");
        //         inventory.AddItem(inventoryItem, 1);
        //         groundItem.OnGrab();
        //     }
        //     if (other.tag == "Equipment") {
        //         Singletons.Main.Interface.OpenMessagePanel();
        //     }
        // }

        // protected void OnTriggerStay(Collider other)
        // {
        //     if (other.tag == "Equipment") {
        //         if (LevelMineStateContext.Instance.IsInState<LevelMineStates.Default>() && this._hasEquipped) {
        //             Singletons.Main.Interface.CloseMessagePanel();
        //             this._lantern.SetActive(true); // Implement Item-Type specific behavior
        //             other.gameObject.SetActive(false);
        //             LevelMineStateContext.Instance.EnterLanternEvent();
        //         }
        //     }
        // }

        // protected void OnTriggerExit(Collider other)
        // {
        //     if (other.tag == "Equipment") {
        //         Singletons.Main.Interface.CloseMessagePanel();
        //     }
        // }

        public void OnExitEntrance(Collider collider)
        {
            if (collider.gameObject == this.gameObject)
            {
                if (LevelMineStateContext.Instance.IsInState<LevelMineStates.Lantern>())
                {
                    LevelMineStateContext.Instance.EnterEarthquakeEvent();
                    this._exitRocks.SetActive(false);
                }
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
