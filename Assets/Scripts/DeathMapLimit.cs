using UnityEngine;

public class DeathMapLimit : MonoBehaviour
{
    public float damage = 0.2f;

    private void OnCollisionStay(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            PlayerHealthSystem playerHealth = collision.transform.GetComponent<PlayerHealthSystem>();

            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
                playerHealth.SetDamageType(PlayerHealthSystem.DamageType.Lava);
            }
        }
    }
}
