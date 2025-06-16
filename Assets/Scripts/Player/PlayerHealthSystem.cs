using UnityEngine;
using UnityEngine.UI;
using System;

public class PlayerHealthSystem : MonoBehaviour
{
    public float maxHealth;
    public float health;

    [SerializeField] private Image healthBarImage;

    public Action onDeath;

    private void Start()
    {
        health = maxHealth;
    }

    private void Update()
    {
        UpdateHealthBar();
    }

    public void TakeDamage(float damage)
    {
        if (!gameObject.activeSelf)
        {
            return;
        }

        health -= damage;

        if (health <= 0)
        {
            health = 0;

            playerDeath();
        }
    }

    private void playerDeath()
    {
        gameObject.SetActive(false);
    }

    private void UpdateHealthBar()
    {
        healthBarImage.fillAmount = health / maxHealth;
    }
}
