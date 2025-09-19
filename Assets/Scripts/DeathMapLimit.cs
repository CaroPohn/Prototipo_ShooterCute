using System;
using System.Collections;
using UnityEngine;

public class DeathMapLimit : MonoBehaviour
{
    public float damage = 0.2f;
    public float postExitDuration = 3f;
    public float damageInterval = 0.1f;

    public static event Action OnExitLava;

    private void OnCollisionStay(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            PlayerHealthSystem playerHealth = collision.transform.GetComponent<PlayerHealthSystem>();

            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
                playerHealth.SetEffectType(PlayerHealthSystem.EffectType.Lava);
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            PlayerHealthSystem playerHealth = collision.transform.GetComponent<PlayerHealthSystem>();

            if (playerHealth != null)
            {
                OnExitLava?.Invoke();
            }
        }
    }
}
