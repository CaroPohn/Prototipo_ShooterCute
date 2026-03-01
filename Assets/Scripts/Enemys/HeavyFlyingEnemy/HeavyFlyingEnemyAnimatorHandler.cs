using UnityEngine;
using System;

public class HeavyFlyingEnemyAnimatorHandler : MonoBehaviour
{
    //private HeavyFlyingEnemy actor;

    public event Action OnFinishFlyingSpawn;
    public event Action OnFlyingHAttack;
    public event Action OnFlyingFall;

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

    public void OnStartFall()
    {
        OnFlyingFall?.Invoke();
    }
}
