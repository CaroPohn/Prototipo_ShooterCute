using UnityEngine;
using UnityEngine.UI;
using System;

public class HealthMeleeSystem : MonoBehaviour
{
    public float maxHealth;
    public float health;

    [SerializeField] private HitEffectController effectControllerScript;

    public int deathCounter;

    private MeleeEnemy meleeEnemy;

    [SerializeField] private GameObject dieParticle;

    public Action onDeath;

    private void Start()
    {
        health = maxHealth;
        deathCounter = 0;

        meleeEnemy = GetComponent<MeleeEnemy>();
    }

    private void Update()
    {
        if (health <= 0)
        {
            health = 0;

            Die();
        }
    }

    public void TakeDamage(float damage)
    {
        effectControllerScript.GetHit();

        if (!gameObject.activeSelf)
        {
            return;
        }

        health -= damage;

        if (health <= 0)
        {
            deathCounter++;

            if (deathCounter == 1)
            {
                Instantiate(dieParticle, transform.position, Quaternion.identity);
                onDeath?.Invoke();
            }
        }
    }

    protected void Die()
    {
        if (meleeEnemy.stopMeleeDieAnimation)
        {
            effectControllerScript.Dissolve();
        }
    }
}
