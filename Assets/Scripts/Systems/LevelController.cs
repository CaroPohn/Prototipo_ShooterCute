using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [Header("Player")]

    [SerializeField] private GameObject player;
    [SerializeField] private HealthSystem playerHealthSystem;
    [SerializeField] private Transform playerSpawnPosition;

    [Header("Enemy")]

    [SerializeField] private GameObject enemy1;
    [SerializeField] private GameObject enemy2;
    [SerializeField] private HealthSystem enemy1HealthSystem;
    [SerializeField] private HealthSystem enemy2HealthSystem;
    [SerializeField] private Transform enemy1SpawnPoint;
    [SerializeField] private Transform enemy2SpawnPoint;

    [Header("Canvas")]

    [SerializeField] private Canvas chooseCanvas;
    [SerializeField] private Canvas gamePlayCanvas;

    private PlayerWeaponChoose playerChooseScript;

    private void Start()
    {
        chooseCanvas.gameObject.SetActive(true);
        gamePlayCanvas.gameObject.SetActive(false);

        playerChooseScript = GetComponent<PlayerWeaponChoose>();

        Time.timeScale = 0.0f;
    }

    private void Update()
    {
        if (playerHealthSystem.health <= 0)
        {
            playerHealthSystem.health = playerHealthSystem.maxHealth;

            Rigidbody rb = player.GetComponent<Rigidbody>();

            rb.AddForce(0, 0, 0);

            player.SetActive(true);

            player.transform.position = playerSpawnPosition.transform.position;
        }

        if (enemy1HealthSystem.health <= 0)
        {
            StartCoroutine(RespawnEnemy1());
        }

            if (enemy2HealthSystem.health <= 0)
        {
            enemy2.SetActive(true);

            enemy2.transform.position = enemy2SpawnPoint.transform.position;

            enemy2.GetComponent<FSM>().ChangeState(enemy2.GetComponent<FSM>().states[0]);

            enemy2HealthSystem.health = enemy2HealthSystem.maxHealth;
        }

        PlayerChoose();
    }

    private void PlayerChoose()
    {
        if (playerChooseScript.playerHasChosen) 
        {
            chooseCanvas.gameObject.SetActive(false);
            gamePlayCanvas.gameObject.SetActive(true);

            Time.timeScale = 1.0f;

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    private IEnumerator RespawnEnemy1()
    {
        if (enemy1HealthSystem.health <= 0)
        {
            enemy1HealthSystem.health = enemy1HealthSystem.maxHealth;

            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();

            enemy1.SetActive(true);

            enemy1.transform.position = enemy1SpawnPoint.transform.position;

            enemy1.GetComponent<FSM>().ChangeState(enemy1.GetComponent<FSM>().states[0]);

            Debug.Log("Se resetearon " + enemy1HealthSystem.health);
        }

        yield return null;
    }

   
}
