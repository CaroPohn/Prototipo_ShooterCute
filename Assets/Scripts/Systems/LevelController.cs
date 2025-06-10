using UnityEngine;

public class LevelController : MonoBehaviour
{
    [Header("Player")]

    [SerializeField] private GameObject player;
    [SerializeField] private HealthSystem playerHealthSystem;
    [SerializeField] private Transform playerSpawnPosition;

    [Header("Canvas")]

    [SerializeField] private Canvas chooseCanvas;
    [SerializeField] private Canvas gamePlayCanvas;

    private void OnEnable()
    {
        PlayerWeaponChoose.OnGunSelected += PlayerChoose;
    }

    private void OnDisable()
    {
        PlayerWeaponChoose.OnGunSelected -= PlayerChoose;
    }

    private void Start()
    {
        chooseCanvas.gameObject.SetActive(true);
        gamePlayCanvas.gameObject.SetActive(false);

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
    }

    private void PlayerChoose()
    {
        chooseCanvas.gameObject.SetActive(false);
        gamePlayCanvas.gameObject.SetActive(true);

        Time.timeScale = 1.0f;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
