using System.Collections.Generic;
using UnityEngine;

public class FlyingHFSM : MonoBehaviour
{
    [SerializeField] private HeavyFlyingEnemy HFEnemy;

    [SerializeField] public List<FlyingHStates> states = new List<FlyingHStates>();

    private FlyingHStates currentState;

    void Start()
    {
        if (states.Count > 0)
            ChangeState(states[0]);
    }

    void Update()
    {
        if (currentState != null)
        {
            currentState.UpdateState(HFEnemy);
        }
    }

    public void ChangeState(FlyingHStates state)
    {
        if (currentState != null)
        {
            currentState.Exit(HFEnemy);
        }

        currentState = state;
        currentState.Enter(HFEnemy);
    }
}
