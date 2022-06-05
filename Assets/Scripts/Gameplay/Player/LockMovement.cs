using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LockMovement : MonoBehaviour {
    [SerializeField]
    public PlayerInput playerInput;
    public bool movementUnlocked = true;

    public void ToggleMovementLock() {
      
        if (movementUnlocked) {
        playerInput.SwitchCurrentActionMap("PlayerLockMovement");
        movementUnlocked = false;
        } else {
            playerInput.SwitchCurrentActionMap("Player");
            movementUnlocked = true;
        }
    }
}
