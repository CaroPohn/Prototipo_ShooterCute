using System.Diagnostics;
using UnityEngine;

public class EnemyAnimationHandler : MonoBehaviour
{
    //Called when the spawn animation is complete
    void SpawnAnimationEnd()
    {
        print("A");
    }
    //Called when the enemy is at the desired attack positon (for example, mouth open)
    void AttackPoseReached()
    {
        print("B");
    }
}
