using System;
using UnityEngine;

public class ZapAnimationHandler : MonoBehaviour
{
    public event Action OnReleaseZapShot;

    public void OnReleaseShot()
    {
        OnReleaseZapShot?.Invoke();

        Debug.Log("Release Event");
    }
}
