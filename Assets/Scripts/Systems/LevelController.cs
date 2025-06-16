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
    [SerializeField] private Canvas winCanvas;

    private void OnEnable()
    {
        PlayerWeaponChoose.OnAbilitySelected += PlayerChoose;
        WinColliderTrigger.OnWinningLevel += WinLevel;
    }

    private void OnDisable()
    {
        PlayerWeaponChoose.OnAbilitySelected -= PlayerChoose;
        WinColliderTrigger.OnWinningLevel -= WinLevel;
    }

    private void Start()
    {
        chooseCanvas.gameObject.SetActive(true);
        gamePlayCanvas.gameObject.SetActive(false);
        winCanvas.gameObject.SetActive(false);

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

    private void WinLevel()
    {
        Time.timeScale = 0.0f;

        winCanvas.gameObject.SetActive(true);
        gamePlayCanvas.gameObject.SetActive(false);
    }
}
