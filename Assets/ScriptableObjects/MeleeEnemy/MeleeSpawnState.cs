using UnityEngine;

[CreateAssetMenu(fileName = "MeleeSpawnState", menuName = "MeleeSO/MeleeSpawnState")]
public class MeleeSpawnState : MeleeStates
{
    public override void Enter(MeleeEnemy meleeEnemy)
    {
        HealthMeleeSystem healthSystem = meleeEnemy.GetComponent<HealthMeleeSystem>();

        meleeEnemy.SetHealthSystemActive(healthSystem, false);

        meleeEnemy.SpawnAnimationHandler();
    }

    public override void UpdateState(MeleeEnemy meleeEnemy)
    {
        meleeEnemy.StopFollowingPlayer(true);

        if (meleeEnemy.stopMeleeSpawnAnimation)
        {
            meleeEnemy.GetComponent<MeleeFSM>().ChangeState(meleeEnemy.GetComponent<MeleeFSM>().states[2]);
        }
    }
}
