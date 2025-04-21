using Unity.VisualScripting;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private HealthSystem healthSystem;
    [SerializeField] private Transform spawnPosition;

    private void Update()
    {
        if (healthSystem.health <= 0)
        {
            healthSystem.health = healthSystem.maxHealth;

            Rigidbody rb = player.GetComponent<Rigidbody>();

            rb.AddForce(0, 0, 0);

            player.SetActive(true);  

            player.transform.position = spawnPosition.transform.position;
        }
    }
}
