using System;
using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class WinColliderTrigger : MonoBehaviour
{
    public static event Action OnWinningLevel;

    [SerializeField] private GameObject egg;

    private void OnCollisionEnter(UnityEngine.Collision collision)
    {
        if (collision.transform.CompareTag("Egg"))
        {
            OnWinningLevel?.Invoke();
        }
    }
}
