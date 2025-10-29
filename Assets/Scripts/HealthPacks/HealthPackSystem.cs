using System;
using System.Collections;
using UnityEngine;

public class HealthPackSystem : MonoBehaviour
{
    public event Action OnGrabingHealthPack;

    public float healingNum;

    private void OnEnable()
    {
        StartCoroutine(LevitatingSoundCorroutine());
    }

    private IEnumerator LevitatingSoundCorroutine()
    {
        yield return new WaitForSeconds(0.1f);
        AkUnitySoundEngine.PostEvent("Lumming_Levitate", gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerHealthSystem>() != null)
        {
            PlayerHealthSystem playerHealthSystem = other.GetComponent<PlayerHealthSystem>();

            if (playerHealthSystem.health + healingNum <= playerHealthSystem.maxHealth) 
            {
                playerHealthSystem.health += healingNum;
                playerHealthSystem.SetEffectType(PlayerHealthSystem.EffectType.Heal);
                AkUnitySoundEngine.PostEvent("Lumming_Levitate_Stop", gameObject);
                OnGrabingHealthPack?.Invoke();
            }
            else if (playerHealthSystem.health + healingNum > playerHealthSystem.maxHealth && playerHealthSystem.health != playerHealthSystem.maxHealth) 
            {
                playerHealthSystem.health = playerHealthSystem.maxHealth;
                playerHealthSystem.SetEffectType(PlayerHealthSystem.EffectType.Heal);
                AkUnitySoundEngine.PostEvent("Lumming_Levitate_Stop", gameObject);
                OnGrabingHealthPack?.Invoke();
            }
        }
    }
}
