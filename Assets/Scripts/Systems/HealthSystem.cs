using UnityEngine;
using UnityEngine.UI;
using System;

public class HealthSystem : MonoBehaviour
{
    public float maxHealth;
    public float health;

    [SerializeField] private Image healthBarImage;
    [SerializeField] private HitEffectController effectControllerScript;

    private int deathCounter;

    public Action onDeath;

    private void Start()
    {
        health = maxHealth;
        deathCounter = 0;
    }

    private void Update()
    {
        UpdateHealthBar();
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
            health = 0;
            deathCounter++;

            Die();
        }
    }

    protected void Die()
    {
        if (deathCounter == 1) 
        {
            onDeath?.Invoke();
        }
        
        Destroy(gameObject);
    }

    private void UpdateHealthBar()
    {
        healthBarImage.fillAmount = health / maxHealth;
    }
}
