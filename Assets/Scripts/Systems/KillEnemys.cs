using System.Collections.Generic;
using UnityEngine;

public class KillEnemys : MonoBehaviour
{
    private List<HealthSystem> enemies;

    private void Update()
    {
        KillAllActualEnemys();
    }

    private void KillAllActualEnemys()
    {
        if (Input.GetKey(KeyCode.K))
        {
            enemies = new List<HealthSystem>(FindObjectsOfType<HealthSystem>());

            foreach (HealthSystem enemy in enemies)
            {
                if (enemy != null)
                {
                    enemy.TakeDamage(enemy.maxHealth);
                }
            }
        }
    }
}
