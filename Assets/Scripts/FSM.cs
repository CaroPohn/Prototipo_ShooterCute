using System.Collections.Generic;
using UnityEngine;

public class FSM : MonoBehaviour
{
    [SerializeField] private GameObject character;

    [SerializeField] private List<States> states = new List<States>();

    private States currentState;

    void Start()
    {
        if (states.Count > 0)
            ChangeState(states[0]);
    }

    void Update()
    {
        if (currentState != null)
        {
            currentState.UpdateState(character);
        }
    }

    public void ChangeState(States state)
    {
        if (currentState != null)
        {
            currentState.Exit(character);
        }

        currentState = state;
        currentState.Enter(character);
    }
}