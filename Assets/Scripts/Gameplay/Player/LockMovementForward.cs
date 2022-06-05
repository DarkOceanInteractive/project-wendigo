using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;

public class LockMovementForward : MonoBehaviour {
   [SerializeField]
    public PlayerInput playerInput;
   [SerializeField]
    public GameObject target;
   [SerializeField]
    private CinemachineVirtualCamera player;
   [SerializeField]
    private CinemachineVirtualCamera focus;

    public bool locked = false;

    public void ToggleCameraAndMovementLock() {
      
        if (!locked) {
            playerInput.SwitchCurrentActionMap("PlayerMoveForward");
            focus.transform.LookAt(target.transform);
            focus.Priority = 10;
            player.Priority = 0;
            locked = true;
        } else {
            playerInput.SwitchCurrentActionMap("Player");
            player.Priority = 10;
            focus.Priority = 0;
            locked = false;
        }
    }
}
