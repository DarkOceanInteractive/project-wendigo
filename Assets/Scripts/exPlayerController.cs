using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static IStates state;
    public string currentState;

    // Initialize States
    internal static IStates sA = new A();
    internal static IStates sB = new B();

    // Start is called before the first frame update
    void Start()
    {
        // Prepare the states
        sA.Entry();
        sB.Entry();

        state = sA; // Initial State
    }

    // Update is called once per frame
    void Update()
    {
        currentState = state.GetType().Name;
        state.Update();
    }
}
