using UnityEngine;

[CreateAssetMenu(fileName = "EnemyFollowState", menuName = "Scriptable Objects/EnemyFollowState")]
public class EnemyFollowState : EnemyStates
{
    public override void UpdateState(PatrolEnemy patrolEnemy)
    {
        patrolEnemy.FollowPlayer();
        patrolEnemy.SetLookAt();
    }
}
