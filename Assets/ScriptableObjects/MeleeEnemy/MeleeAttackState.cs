using UnityEngine;

[CreateAssetMenu(fileName = "MeleeAttackState", menuName = "MeleeSO/MeleeAttackState")]
public class MeleeAttackState : MeleeStates
{
    public override void UpdateState(MeleeEnemy meleeEnemy)
    {
        meleeEnemy.AttackAnimationHandler();

        meleeEnemy.SetLookAt();

        meleeEnemy.GetComponent<MeleeFSM>().ChangeState(meleeEnemy.GetComponent<MeleeFSM>().states[2]);
    }
}
