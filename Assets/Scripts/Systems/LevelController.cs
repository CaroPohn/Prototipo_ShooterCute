using UnityEngine;

public class LevelController : MonoBehaviour
{
    [Header("Player")]

    [SerializeField] private GameObject player;
    [SerializeField] private PlayerHealthSystem playerHealthSystem;
    [SerializeField] private Transform playerSpawnPosition;

    [Header("Canvas")]

    [SerializeField] private Canvas chooseCanvas;
    [SerializeField] private Canvas gamePlayCanvas;
    [SerializeField] private Canvas winCanvas;

    [SerializeField] private GameObject WinEggText;
    [SerializeField] private GameObject StartWavesEggText;

    private void OnEnable()
    {
        WinColliderTrigger.OnWinningLevel += WinLevel;
        WaveManager.OnWinningAllWaves += ActivateEggWinText;
        EggInteraction.OnInteractWithEgg += DeactivateEggStartWavesText;
    }

    private void OnDisable()
    {
        WinColliderTrigger.OnWinningLevel -= WinLevel;
        WaveManager.OnWinningAllWaves -= ActivateEggWinText;
        EggInteraction.OnInteractWithEgg -= DeactivateEggStartWavesText;
    }

    private void Start()
    {
        gamePlayCanvas.gameObject.SetActive(true);
        winCanvas.gameObject.SetActive(false);
        WinEggText.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
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

    private void WinLevel()
    {
        Time.timeScale = 0.0f;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;

        winCanvas.gameObject.SetActive(true);
        gamePlayCanvas.gameObject.SetActive(false);
    }

    private void ActivateEggWinText()
    {
        WinEggText.SetActive(true);
    }

    private void DeactivateEggStartWavesText()
    {
        StartWavesEggText.SetActive(false);
    }
}
