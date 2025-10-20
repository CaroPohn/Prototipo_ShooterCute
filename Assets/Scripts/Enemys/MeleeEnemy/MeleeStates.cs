using System;
using UnityEngine;

[Serializable]
public class MeleeStates : ScriptableObject
{
    public virtual void Enter(MeleeEnemy character) { }

    public virtual void UpdateState(MeleeEnemy character) { }

    public virtual void Exit(MeleeEnemy character) { }
}
