using System;
using UnityEngine;

public class ArmsAnimatorHandler : MonoBehaviour
{
    public static event Action OnThrowToIdle;
    
    public static event Action OnSqueezeJhonny;
    public static event Action OnSqueezeJhonnyToGun;
    
    public void OnReleaseToIdle()
    {
        OnThrowToIdle?.Invoke();
    }

    public void OnSqueeze()
    {
        OnSqueezeJhonny?.Invoke();
    }

    public void OnSqueezeToGun()
    {
        OnSqueezeJhonnyToGun?.Invoke();
    }
}
