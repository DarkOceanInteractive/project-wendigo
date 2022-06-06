using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class CameraSwitcher : MonoBehaviour {
    [SerializeField]
    private CinemachineVirtualCamera player;
    [SerializeField]
    private CinemachineVirtualCamera focus;
    [SerializeField]
    private float seconds;
    [SerializeField]
    public GameObject target;
    private bool playerCamera = true;

    public void SwitchCamera() {
        if (playerCamera) {
            focus.transform.LookAt(target.transform);
            player.Priority = 0;
            focus.Priority = 1;
            Invoke("SwitchCamera", seconds);
        } else {
            player.Priority = 1;
            focus.Priority = 0;
        }
        playerCamera = !playerCamera;
    }
}
