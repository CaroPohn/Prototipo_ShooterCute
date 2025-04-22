using UnityEngine;

public class DeathMapLimit : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            HealthSystem playerHealth = collision.transform.GetComponent<HealthSystem>();

            playerHealth.TakeDamage(playerHealth.maxHealth);
        }
    }
}
