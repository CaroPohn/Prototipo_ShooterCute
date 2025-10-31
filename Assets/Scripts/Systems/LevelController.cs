using System;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [Header("Player")]

    [SerializeField] private GameObject player;
    [SerializeField] private PlayerHealthSystem playerHealthSystem;
    [SerializeField] private Transform playerSpawnPosition;
    [SerializeField] private InputReader inputReader;

    public bool isGamePaused;

    static public event Action OnGamePaused;
    static public event Action OnGameUnpaused;

    [Header("Canvas")]

    [SerializeField] private Canvas chooseCanvas;
    [SerializeField] private Canvas gamePlayCanvas;
    [SerializeField] private Canvas winCanvas;

    [SerializeField] private GameEndScreenUI gameEndScreenUI;

    [Header("Egg")]
    [SerializeField] private EggInteraction eggInteractionScript;

    private void OnEnable()
    {
        Time.timeScale = 1.0f;

        WinColliderTrigger.OnWinningLevel += WinLevel;
        inputReader.OnPause += PauseGame;
        playerHealthSystem.SetEffectType(PlayerHealthSystem.EffectType.None);
    }

    private void OnDisable()
    {
        WinColliderTrigger.OnWinningLevel -= WinLevel;
        inputReader.OnPause -= PauseGame;
    }

    private void Start()
    {
        Application.targetFrameRate = 144;

        gamePlayCanvas.gameObject.SetActive(true);
        winCanvas.gameObject.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        isGamePaused = false;
    }

    private void Update()
    {
        if (playerHealthSystem.health <= 0)
        {
            LoseLevel();
        }
    }

    private void LoseLevel()
    {
        Time.timeScale = 0.0f;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;

        gameEndScreenUI.PlayMissionFailedAnimation();
    }

    private void WinLevel()
    {
        Time.timeScale = 0.0f;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;

        gameEndScreenUI.PlayMissionAccomplishedAnimation();

        gamePlayCanvas.gameObject.SetActive(false);
    }

    public void PauseGame()
    {
        isGamePaused = !isGamePaused;

        if (isGamePaused)
        {
            Time.timeScale = 0f;
            OnGamePaused?.Invoke();
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Time.timeScale = 1f;
            OnGameUnpaused?.Invoke();
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
