using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStates
{
    void Entry();
    void Update();
    void FixedUpdate();
    void Exit();
}
