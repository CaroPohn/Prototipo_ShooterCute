using System;
using UnityEngine;

public class WinColliderTrigger : MonoBehaviour
{
    public static event Action OnWinningLevel;

    [SerializeField] private GameObject egg;

    private void OnTriggerEnter(Collider other)
    {
        EggInteraction eggInteraction = other.GetComponentInChildren<EggInteraction>();

        if (eggInteraction != null && eggInteraction.gameObject == egg)
        {
            OnWinningLevel?.Invoke();
            Debug.Log("¡Ganaste el nivel!");
        }
    }
}
