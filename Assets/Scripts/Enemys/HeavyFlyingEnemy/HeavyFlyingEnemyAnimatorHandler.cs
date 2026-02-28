using UnityEngine;
using System;

public class HeavyFlyingEnemyAnimatorHandler : MonoBehaviour
{
    //private HeavyFlyingEnemy actor;

    public event Action OnFinishFlyingSpawn;
    public event Action OnFlyingHAttack;

    //private void Start()
    //{
    //    actor = GetComponentInParent<HeavyFlyingEnemy>();
    //}

    public void OnAttack()
    {
        OnFlyingHAttack?.Invoke();
    }

    public void OnFinishSpawn()
    {
        OnFinishFlyingSpawn?.Invoke();
    }
}
