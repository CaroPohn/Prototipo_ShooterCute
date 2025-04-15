using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    public float maxHealth;
    private float health;

    [SerializeField] private Image healthBarImage;

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
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    protected void Die()
    {
        gameObject.SetActive(false);
    }

    private void UpdateHealthBar()
    {
        healthBarImage.fillAmount = health / maxHealth;
    }
}
