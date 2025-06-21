using System.Collections;
using UnityEngine;

public class DeathMapLimit : MonoBehaviour
{
    public float damage = 0.2f;
    public float postExitDuration = 3f;
    public float damageInterval = 0.1f;

    private float timeElapsedSinceExit;

    private void Update()
    {
        timeElapsedSinceExit += Time.deltaTime;
    }

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

    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            Debug.Log("Player salió del DeathMapLimit");

            PlayerHealthSystem playerHealth = collision.transform.GetComponent<PlayerHealthSystem>();

            if (playerHealth != null)
            {
                StartCoroutine(DamageOverTimeAfterExit(playerHealth));
            }
        }
    }

    private IEnumerator DamageOverTimeAfterExit(PlayerHealthSystem playerHealth)
    {
        timeElapsedSinceExit = 0;

        while (timeElapsedSinceExit < postExitDuration)
        {
            playerHealth.TakeDamage(damage);
            playerHealth.SetDamageType(PlayerHealthSystem.DamageType.Lava);

            yield return new WaitForSeconds(damageInterval);
        }
    }
}
