using UnityEngine;

namespace ProjectWendigo
{
    public class PlayerController : MonoBehaviour
    {
        private Transform _cameraTransform;
        [SerializeField] private CharacterController _characterController;
        private Vector3 _motion = Vector3.zero;
        public InventoryObject inventory;
        //public ItemPickup itemPickup;
        
        protected void Awake()
        {
            this._cameraTransform = Camera.main.transform;
        }

        protected void OnApplicationQuit() {
            inventory.Container.Items = new InventorySlot[35];
        }
        
        protected void OnTriggerEnter(Collider other) {
            // Makes player add item to inventory when walking over
            var item = other.GetComponent<GroundItem>();
            if (item) {
                Item _item = new Item(item.item);
                Debug.Log("Grabbed" + _item.Name + _item.Id);
                inventory.AddItem(_item, 1);
                Destroy(other.gameObject);
            }
        }

        protected void Update()
        {
            //Save Inventory by pressing R
            if (Singletons.Main.Input.PlayerSavedInventory) {
                inventory.Save();
            }
            //Load Inventory by pressing T
            if (Singletons.Main.Input.PlayerLoadedInventory) {
                inventory.Load();
            }
            //Grab Item by pressing F
            if (Singletons.Main.Input.PlayerGrabbedItem) {
                //Not implemented
            }

            if (this._motion != Vector3.zero)
            {
                this._motion = this.TransformToCameraView(this._motion);
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

        /// <summary>
        /// Transform a direction vector along the camera view direction.
        /// </summary>
        /// <param name="direction">Direction vector to transform</param>
        /// <returns>Direction vector transformed along the camera view direction</returns>
        private Vector3 TransformToCameraView(Vector3 direction)
        {
            // Extract the camera forward direction along the x and z axes.
            Vector3 cameraForward = this._cameraTransform.forward;
            Vector3 horizontalCameraForward = new Vector3(cameraForward.x, 0f, cameraForward.z).normalized;

            // Extract the camera right direction along the x and z axes.
            Vector3 cameraRight = this._cameraTransform.right;
            Vector3 horizontalCameraRight = new Vector3(cameraRight.x, 0f, cameraRight.z).normalized;

            return horizontalCameraForward * direction.z
              + Vector3.up * direction.y
              + horizontalCameraRight * direction.x;
        }

    }
}
