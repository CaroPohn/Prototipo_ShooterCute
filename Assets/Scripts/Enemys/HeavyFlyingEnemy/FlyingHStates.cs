using System;
using UnityEngine;

public class FlyingHStates : ScriptableObject
{
    public virtual void Enter(HeavyFlyingEnemy character) { }

    public virtual void UpdateState(HeavyFlyingEnemy character) { }

    public virtual void Exit(HeavyFlyingEnemy character) { }
}
