using UnityEngine;
using UnityEngine.UI;
using System;

public class HealthSystem : MonoBehaviour
{
    public float maxHealth;
    public float health;

    [SerializeField] private HitEffectController effectControllerScript;

    public int deathCounter;

    [SerializeField] private GameObject dieParticle;

    public Action onDeath;

    private void Start()
    {
        health = maxHealth;
        deathCounter = 0;
    }

    private void OnEnable()
    {
        PatrolEnemy.onStopDieAnimation += Die;
        MeleeEnemy.onStopDieAnimation += Die;
    }

    private void OnDisable()
    {
        PatrolEnemy.onStopDieAnimation -= Die;
        MeleeEnemy.onStopDieAnimation -= Die;
    }

    private void Update()
    {
        if (health <= 0)
        {
            health = 0;
            
            Die(gameObject);
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

    protected void Die(GameObject sender)
    {
        if (sender == gameObject)
        {
            effectControllerScript.Dissolve();
        }      
    }
}
