using UnityEngine;

[CreateAssetMenu(fileName = "MeleeDieState", menuName = "MeleeSO/MeleeDieState")]
public class MeleeDieState : MeleeStates
{
    public override void Enter(MeleeEnemy meleeEnemy)
    {
        meleeEnemy.StopFollowingPlayer(true);

        meleeEnemy.DeactivateColliders();

        meleeEnemy.DieAnimationHandler();
    }
}
