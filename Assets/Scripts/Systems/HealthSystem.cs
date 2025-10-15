using UnityEngine;
using UnityEngine.UI;
using System;

public class HealthSystem : MonoBehaviour
{
    public float maxHealth;
    public float health;

    [SerializeField] private Image healthBarImage;
    [SerializeField] private HitEffectController effectControllerScript;

    public int deathCounter;

    private PatrolEnemy patrolEnemy;

    [SerializeField] private GameObject dieParticle;

    public Action onDeath;

    private void Start()
    {
        health = maxHealth;
        deathCounter = 0;

        patrolEnemy = GetComponent<PatrolEnemy>();
    }

    private void Update()
    {
        UpdateHealthBar();

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
        if (patrolEnemy.stopDieAnimation)
        {
            effectControllerScript.Dissolve();
        }     
    }

    private void UpdateHealthBar()
    {
        healthBarImage.fillAmount = health / maxHealth;
    }
}
