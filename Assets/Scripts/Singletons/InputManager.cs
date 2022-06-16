using UnityEngine;
using UnityEngine.InputSystem;
using System;

namespace ProjectWendigo
{
    public class InputManager : MonoBehaviour
    {
        private PlayerControls _input;

        private Func<Vector2, Vector2> _playerMovementFilter = movement => movement;

        // General helpers
        public Vector2 MousePosition => Mouse.current.position.ReadValue();

        // Player inputs helpers
        public bool PlayerIsCrouching => this._input.Player.Crouch.IsPressed();
        public bool PlayerIsMoving => this.PlayerMovement != Vector2.zero;
        // public bool PlayerJumped => this._input.Player.Jump.WasPressedThisFrame();
        public bool PlayerStartedCrouching => this._input.Player.Crouch.WasPressedThisFrame();
        // public bool PlayerStartedSprinting => this._input.Player.Sprint.WasPressedThisFrame();
        public bool PlayerStoppedCrouching => this._input.Player.Crouch.WasReleasedThisFrame();
        // public bool PlayerStoppedSprinting => this._input.Player.Sprint.WasReleasedThisFrame();
        // public bool PlayerToggledInventory => this._input.Player.Toggleinventory.WasPressedThisFrame();
        public bool PlayerSavedInventory => this._input.Player.Saveinventory.WasPressedThisFrame();
        public bool PlayerLoadedInventory => this._input.Player.Loadinventory.WasPressedThisFrame();
        public bool PlayerGrabbedItem => this._input.Player.Grab.WasPressedThisFrame();
        public Vector2 PlayerLook => this._input.Player.Look.ReadValue<Vector2>();
        public Vector2 PlayerMovement => this._playerMovementFilter(this._input.Player.Move.ReadValue<Vector2>());
        public bool PlayerToggleFade => this._input.Player.ToggleFadeEffect.WasPressedThisFrame();
        public bool PlayerToggledNotebookJournal => this._input.Player.ToggleNotebookJournal.WasPressedThisFrame();
        public bool PlayerToggledNotebookMaps => this._input.Player.ToggleNotebookMaps.WasPressedThisFrame();
        public bool PlayerToggledNotebookArchive => this._input.Player.ToggleNotebookFindings.WasPressedThisFrame();
        public bool PlayerExittedUI => this._input.Player.ExitUI.WasPressedThisFrame();
        public bool PlayerInteracted => this._input.Player.Interact.WasPressedThisFrame();

        public bool PlayerPaused => this._input.UI.Pause.WasPressedThisFrame();

        public bool PlayerJumped => false;
        public bool PlayerStartedSprinting => false;
        public bool PlayerStoppedSprinting => false;
        public bool PlayerToggledInventory => false;

        // Lighting input helpers
        public bool LightingToggled => this._input.Lighting.Togglelight.WasPressedThisFrame();

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

        public void EnableAction(string name)
        {
            this._input.FindAction(name)?.Enable();
        }

        public void DisableAction(string name)
        {
            this._input.FindAction(name)?.Disable();
        }

        public void LockPlayerMovements()
        {
            this.DisableAction("Player/Move");
        }

        public void LockPlayerMovements(float time)
        {
            this.LockPlayerMovements();
            Invoke(nameof(this.UnlockPlayerMovements), time);
        }

        public void UnlockPlayerMovements()
        {
            this.EnableAction("Player/Move");
        }

        public void SetPlayerMovementFilter(Func<Vector2, Vector2> filter)
        {
            this._playerMovementFilter = filter;
        }

        public void ResetPlayerMovementFilter()
        {
            this._playerMovementFilter = movement => movement;
        }

        protected void Awake()
        {
            this._input = new PlayerControls();
            this._input.Player.Enable();
            this._input.UI.Enable();
            this._input.Lighting.Enable();
            this.SetDefaultCursor();
            this.HideCursor();
        }
    }
}
