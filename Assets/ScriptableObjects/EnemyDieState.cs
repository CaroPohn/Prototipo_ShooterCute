using UnityEngine;

[CreateAssetMenu(fileName = "EnemyDieState", menuName = "Scriptable Objects/EnemyDieState")]
public class EnemyDieState : EnemyStates
{
    public override void Enter(PatrolEnemy patrolEnemy)
    {
        patrolEnemy.StopFollowingPlayer(true);

        patrolEnemy.DieAnimationHandler();
    }
}