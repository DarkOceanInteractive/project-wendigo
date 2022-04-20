using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveObjectDown : MonoBehaviour
{
    [SerializeField]
    public float amount;
    private int counter = 0;
    
    //Move rocks down by 10
    void OnTriggerEnter(Collider other) {
        if (other.tag == "Player" && counter == 0)
        Debug.Log("Rock fell");
        counter++;
        transform.position -= new Vector3(0,amount,0);
    }
}
