using UnityEngine;
using System;
using System.Collections.Generic;

public abstract class State : MonoBehaviour
{

    public event Action<State, string, Dictionary<string, object>> OnTransition;

    // Virtual methods for the State Machine to call
    public virtual void Enter(Dictionary<string, object> extraArgs = null) { print("Entering state " + name); }
    public virtual void Exit() { print("Exiting state " + name );  }
    public virtual void LogicUpdate() { }  
    public virtual void PhysicsUpdate() { } 

    // Helper method to trigger the transition
    protected void TransitionTo(string newStateName, Dictionary<string, object> extraArgs = null)
    {
        OnTransition?.Invoke(this, newStateName, extraArgs);
    }
}