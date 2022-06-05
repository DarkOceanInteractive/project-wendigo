using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class CameraSwitcher : MonoBehaviour {
    [SerializeField]
    private InputAction action;
    [SerializeField]
    private CinemachineVirtualCamera player;
    [SerializeField]
    private CinemachineVirtualCamera target;
    [SerializeField]
    private float seconds;
    private bool playerCamera = true;

    public void OnEnable() {
        action.Enable();
    }

    public void OnDisable() {
        action.Disable();
    }

    public void Start() {
        action.performed += _ => SwitchCamera();
    }

    public void SwitchCamera() {
        if (playerCamera) {
            player.Priority = 0;
            target.Priority = 1;
            Invoke("SwitchCamera", seconds);
        } else {
            player.Priority = 1;
            target.Priority = 0;
        }
        playerCamera = !playerCamera;
    }
}
