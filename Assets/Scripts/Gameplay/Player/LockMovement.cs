using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LockMovement : MonoBehaviour {
    [SerializeField]
    public PlayerInput playerInput;
    [SerializeField]
    private float seconds;

    public bool locked = false;

    public void ToggleMovementLock() {
      
        if (!locked) {
            playerInput.SwitchCurrentActionMap("PlayerLockMovement");
            locked = true;
        } else {
            playerInput.SwitchCurrentActionMap("Player");
            locked = false;
        }
        Invoke("ToggleMovementLock", seconds);
    }
}
