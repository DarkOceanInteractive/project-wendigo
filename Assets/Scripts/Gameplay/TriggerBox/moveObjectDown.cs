using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveObjectDown : MonoBehaviour
{
    //Move rocks down by 10
    void OnTriggerEnter(Collider other) {
        Debug.Log("yasss queen!");
        this.transform.position -= new Vector3(0,10,0);
    }
}
