using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EnemyStates : ScriptableObject
{
    public virtual void Enter(PatrolEnemy character) { }

    public virtual void UpdateState(PatrolEnemy character) { }

    public virtual void Exit(PatrolEnemy character) { }
}
