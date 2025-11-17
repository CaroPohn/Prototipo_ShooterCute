using UnityEngine;

public class RootDamageEffect : MonoBehaviour
{
    public float damage = 0.2f;
    public float damageInterval = 0.1f;

    private void OnCollisionStay(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            PlayerHealthSystem playerHealth = collision.transform.GetComponent<PlayerHealthSystem>();

            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
                playerHealth.SetEffectType(PlayerHealthSystem.EffectType.EnemyDamage);
            }
        }
    }
}
