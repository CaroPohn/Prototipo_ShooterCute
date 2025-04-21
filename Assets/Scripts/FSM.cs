using System.Collections.Generic;
using UnityEngine;

public class FSM : MonoBehaviour
{
    [SerializeField] private PatrolEnemy patrolEnemy;

    [SerializeField] public List<EnemyStates> states = new List<EnemyStates>();

    private EnemyStates currentState;

    void Start()
    {
        if (states.Count > 0)
            ChangeState(states[0]);
    }

    void Update()
    {
        if (currentState != null)
        {
            currentState.UpdateState(patrolEnemy);
        }
    }

    public void ChangeState(EnemyStates state)
    {
        if (currentState != null)
        {
            currentState.Exit(patrolEnemy);
        }

        currentState = state;
        currentState.Enter(patrolEnemy);
    }
}