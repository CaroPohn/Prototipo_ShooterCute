using System;
using UnityEngine;

public class ArmsAnimatorHandler : MonoBehaviour
{
    public static event Action OnThrowToIdle;

    public void OnReleaseToIdle()
    {
        OnThrowToIdle?.Invoke();
    }
}
