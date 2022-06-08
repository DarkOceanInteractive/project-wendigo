using UnityEngine;
using UnityEngine.InputSystem;

namespace ProjectWendigo
{
    public class InputManager : MonoBehaviour
    {
        private PlayerInput _playerInput;

        // General helpers
        public Vector2 MousePosition => Mouse.current.position.ReadValue();

        // Player inputs helpers
        public bool PlayerIsCrouching => this._playerInput.actions["Crouch"].IsPressed();
        public bool PlayerIsMoving => this.PlayerMovement != Vector2.zero;
        // public bool PlayerJumped => this._playerInput.actions["Jump"].WasPressedThisFrame();
        public bool PlayerStartedCrouching => this._playerInput.actions["Crouch"].WasPressedThisFrame();
        // public bool PlayerStartedSprinting => this._playerInput.actions["Sprint"].WasPressedThisFrame();
        public bool PlayerStoppedCrouching => this._playerInput.actions["Crouch"].WasReleasedThisFrame();
        // public bool PlayerStoppedSprinting => this._playerInput.actions["Sprint"].WasReleasedThisFrame();
        // public bool PlayerToggledInventory => this._playerInput.actions["Toggle inventory"].WasPressedThisFrame();
        public bool PlayerSavedInventory => this._playerInput.actions["Save inventory"].WasPressedThisFrame();
        public bool PlayerLoadedInventory => this._playerInput.actions["Load inventory"].WasPressedThisFrame();
        public bool PlayerGrabbedItem => this._playerInput.actions["Grab"].WasPressedThisFrame();
        public Vector2 PlayerLook => this._playerInput.actions["Look"].ReadValue<Vector2>();
        public Vector2 PlayerMovement => this._playerInput.actions["Move"].ReadValue<Vector2>();
        public bool PlayerToggleFade => this._playerInput.actions["Toggle FadeEffect"].WasPressedThisFrame();
        public bool PlayerToggledNotebookJournal => this._playerInput.actions["Toggle Notebook Journal"].WasPressedThisFrame();
        public bool PlayerToggledNotebookMaps => this._playerInput.actions["Toggle Notebook Maps"].WasPressedThisFrame();
        public bool PlayerToggledNotebookArchive => this._playerInput.actions["Toggle Notebook Findings"].WasPressedThisFrame();
        public bool PlayerExittedUI => this._playerInput.actions["Exit UI"].WasPressedThisFrame();

        public bool PlayerJumped => false;
        public bool PlayerStartedSprinting => false;
        public bool PlayerStoppedSprinting => false;
        public bool PlayerToggledInventory => false;

        // Lighting input helpers
        public bool LightingToggled => this._playerInput.actions["Toggle light"].WasPressedThisFrame();

        [SerializeField] private CursorSettings _defaultCursor;
        private CursorSettings _cursor;

        public CursorSettings GetCursor()
        {
            return this._cursor;
        }

        public void SetDefaultCursor()
        {
            this.SetCursor(this._defaultCursor);
        }

        public void SetCursor(CursorSettings cursor)
        {
            Cursor.SetCursor(cursor.texture, cursor.hotspot, CursorMode.Auto);
            this._cursor = cursor;
        }

        public void HideCursor()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        public void ShowCursor()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        protected void Awake()
        {
            this._playerInput = this.GetComponent<PlayerInput>();
            this._playerInput.actions.FindActionMap("Player").Enable();
            this._playerInput.actions.FindActionMap("UI").Enable();
            this._playerInput.actions.FindActionMap("Lighting").Enable();

            this.SetDefaultCursor();
            this.HideCursor();
        }
    }
}
