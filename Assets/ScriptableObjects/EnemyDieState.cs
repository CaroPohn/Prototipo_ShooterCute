using UnityEngine;

[CreateAssetMenu(fileName = "NewScriptableObjectScript", menuName = "Scriptable Objects/NewScriptableObjectScript")]
public class EnemyDieState : EnemyStates
{
    public override void Enter(PatrolEnemy patrolEnemy)
    {
        patrolEnemy.GetComponent<FSM>().ChangeState(patrolEnemy.GetComponent<FSM>().states[0]);
    }
}
