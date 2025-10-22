using System.Collections.Generic;
using UnityEngine;

public class MeleeFSM : MonoBehaviour
{
    [SerializeField] private MeleeEnemy meleeEnemy;

    [SerializeField] public List<MeleeStates> states = new List<MeleeStates>();

    private MeleeStates currentState;

    void Start()
    {
        if (states.Count > 0)
            ChangeState(states[0]);
    }

    void Update()
    {
        if (currentState != null)
        {
            currentState.UpdateState(meleeEnemy);
        }
    }

    public void ChangeState(MeleeStates state)
    {
        if (currentState != null)
        {
            currentState.Exit(meleeEnemy);
        }

        currentState = state;
        currentState.Enter(meleeEnemy);
    }
}
