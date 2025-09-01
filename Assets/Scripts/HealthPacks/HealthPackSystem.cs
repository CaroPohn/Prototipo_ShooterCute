using System;
using UnityEngine;

public class HealthPackSystem : MonoBehaviour
{
    public event Action OnGrabingHealthPack;

    public float healingNum;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerHealthSystem>() != null)
        {
            PlayerHealthSystem playerHealthSystem = other.GetComponent<PlayerHealthSystem>();

            if (playerHealthSystem.health + healingNum <= playerHealthSystem.maxHealth) 
            {
                playerHealthSystem.health += healingNum;
                playerHealthSystem.SetEffectType(PlayerHealthSystem.EffectType.Heal);
                OnGrabingHealthPack?.Invoke();
            }
            else if (playerHealthSystem.health + healingNum > playerHealthSystem.maxHealth && playerHealthSystem.health != playerHealthSystem.maxHealth) 
            {
                playerHealthSystem.health = playerHealthSystem.maxHealth;
                playerHealthSystem.SetEffectType(PlayerHealthSystem.EffectType.Heal);
                OnGrabingHealthPack?.Invoke();
            }
        }
    }
}
