using System;
using UnityEngine;

public class StationAnimationHandler : MonoBehaviour
{
    public event Action OnFinishStationDeathAnimation; 

    public void OnLettingPlayerGrabEgg()
    {
        OnFinishStationDeathAnimation.Invoke();
    }
}
