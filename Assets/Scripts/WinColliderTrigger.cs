using System;
using UnityEngine;

public class WinColliderTrigger : MonoBehaviour
{
    public static event Action OnWinningLevel;

    [SerializeField] private GameObject egg;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);

        if (other.GetComponentInChildren<EggInteraction>() != null)
        {
            Debug.Log("Entró el player al win collider");
            OnWinningLevel?.Invoke();
        }
        else
        {
            Debug.Log("No encuentra el egg interaction");
        }

    }
}
