using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class States : ScriptableObject
{
    [Tooltip("List of states that this state can transition to.")]
    [SerializeField] public List<States> states = new List<States>();

    public virtual void Enter(GameObject character) { }

    public virtual void UpdateState(GameObject character) { }

    public virtual void Exit(GameObject character) { }
}
