using System;
using UnityEngine;

public class WinColliderTrigger : MonoBehaviour
{
    public static event Action OnWinningLevel;

    [SerializeField] private GameObject egg;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInChildren<EggInteraction>() != null)
        {
            OnWinningLevel?.Invoke();
        }
    }
}
