using System;
using UnityEngine;

public class BombAnimationHandler : MonoBehaviour
{
    public event Action OnShotEnd;

    public void OnFinishShot()
    {
        OnShotEnd?.Invoke();
    }
}
