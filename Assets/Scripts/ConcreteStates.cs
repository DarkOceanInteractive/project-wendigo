using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A : IStates
{
    public void Entry()
    {
        // Entry should be ran before the first frame update
    }

    public void Update()
    {
        // Update should be ran on frame updates
    }

    public void FixedUpdate()
    {
        // FixedUpdate should be ran on fixed frame updates, the frequency being frame-rate independent
    }

    public void Exit()
    {
        // Exist should be ran on the last frame of a state, before transitioning to another
    }
}

public class B : IStates
{
    public void Entry()
    {
        // Entry should be ran before the first frame update
    }

    public void Update()
    {
        // Update should be ran on frame updates
    }

    public void FixedUpdate()
    {
        // FixedUpdate should be ran on fixed frame updates, the frequency being frame-rate independent
    }

    public void Exit()
    {
        // Exist should be ran on the last frame of a state, before transitioning to another
    }
}
