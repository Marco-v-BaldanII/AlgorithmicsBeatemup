using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;

public class StateMachine : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private State _initialState;
    [SerializeField] private bool _reInitializeOnLoad = false;
    [SerializeField] private bool _reEnterIfTransitionToSameState = true;

    // Public variable logic
    public bool IsTurn = false;

    // State Management
    public State CurrentState { get; private set; }
    private Dictionary<string, State> _states = new Dictionary<string, State>();

    // Events (Signals)
    public event Action<string> OnChangeState;
    public event Action OnInitialize;

    // C# Property for name_current_state
    public string NameCurrentState
    {
        get
        {
            if (CurrentState != null)
            {
                return CurrentState.gameObject.name.ToLower();
            }
            return "";
        }
    }

    private void Start()
    {
        // 1. Loop through children and add States to dictionary
        // We iterate specifically over the Transform children to mimic get_children()
        foreach (Transform child in transform)
        {
            State stateComponent = child.GetComponent<State>();

            if (stateComponent != null)
            {
                // Key is the Game Object name
                _states[child.name.ToLower()] = stateComponent;

                // Connect the function to the transition event (Signal connection)
                stateComponent.OnTransition += OnChildTransition;
            }
        }

        // 2. Initialize the state
        if (_initialState != null)
        {
            // Note: FileManager check is commented out as it's custom to your project
            bool isLoading = false; // Replace with: FileManager.IsLoading() 

            if ((CurrentState == null && !isLoading) || _reInitializeOnLoad)
            {
                EnterState(_initialState);
                OnInitialize?.Invoke();
            }
        }
    }

    private void Update()
    {
        if (CurrentState != null)
        {
            CurrentState.LogicUpdate();
        }
    }

    private void FixedUpdate()
    {
        if (CurrentState != null)
        {
            CurrentState.PhysicsUpdate();
        }
    }

    public void OnChildTransition(State state, string newStateName, Dictionary<string, object> extraArgs = null)
    {
        if (state != CurrentState) return;

        // Get the new state from the dictionary
        string stateKey = newStateName.ToLower();

        if (!_states.ContainsKey(stateKey))
        {
            Debug.LogError($"StateMachine: Couldn't find state '{stateKey}'");
            return;
        }

        State newState = _states[stateKey];

        // Check re-entry logic
        if (!_reEnterIfTransitionToSameState && newState == state) return;

        // Perform the switch
        if (CurrentState != null)
        {
            CurrentState.Exit();
        }

        CurrentState = newState;

        // Pass extra args if they exist
        CurrentState.Enter(extraArgs);

        OnChangeState?.Invoke(newStateName);
    }

    // Helper to enter state cleanly on initialization
    private void EnterState(State newState)
    {
        CurrentState = newState;
        CurrentState.Enter();
        OnChangeState?.Invoke(newState.gameObject.name);
    }


    private void OnDestroy()
    {
        // Clean up events to prevent memory leaks (good practice in C#)
        foreach (var state in _states.Values)
        {
            state.OnTransition -= OnChildTransition;
        }
    }
}