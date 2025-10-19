using UnityEngine;

[CreateAssetMenu(fileName = "MeleeIdleState", menuName = "MeleeSO/MeleeIdleState")]
public class MeleeIdleState : MeleeStates
{
    public override void Enter(MeleeEnemy meleeEnemy)
    {
        meleeEnemy.GetComponent<MeleeFSM>().ChangeState(meleeEnemy.GetComponent<MeleeFSM>().states[1]);
    }
}
