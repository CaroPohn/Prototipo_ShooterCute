using System;
using System.Collections.Generic;
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
    [SerializeField] private Canvas gamePlayVFXCanvas;
    [SerializeField] private Canvas winCanvas;

    [SerializeField] private GameEndScreenUI gameEndScreenUI;

    [Header("Egg")]
    [SerializeField] private EggInteraction eggInteractionScript;

    public static event Action OnDefeat;
    public static event Action OnWin;

    private List<GameObject> desintegrateableObjects;

    [SerializeField] private GameObject winCollider;

    private bool isLevelLost = false;

    private void OnEnable()
    {
        Time.timeScale = 1.0f;

        winCollider.SetActive(false);

        WinColliderTrigger.OnWinningLevel += WinLevel;
        EggInteraction.OnGrabbingEgg += ChangeObjetiveToWinCollider;

        WaveManager.OnWinningAllWaves += DesintegrateObjects;

        EggShield.OnFinishDesintegrate += DestroyDesintegrateObjects;

        inputReader.OnPause += PauseGame;
        playerHealthSystem.SetEffectType(PlayerHealthSystem.EffectType.None);
    }

    private void OnDisable()
    {
        WinColliderTrigger.OnWinningLevel -= WinLevel;
        EggInteraction.OnGrabbingEgg -= ChangeObjetiveToWinCollider;

        WaveManager.OnWinningAllWaves -= DesintegrateObjects;

        EggShield.OnFinishDesintegrate -= DestroyDesintegrateObjects;

        inputReader.OnPause -= PauseGame;
    }

    private void Start()
    {
        Application.targetFrameRate = 144;

        gamePlayCanvas.gameObject.SetActive(true);
        gamePlayVFXCanvas.gameObject.SetActive(true);
        winCanvas.gameObject.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        isGamePaused = false;
    }

    private void Update()
    {
        if (playerHealthSystem.health <= 0 && !isLevelLost)
        {
            LoseLevel();
        }
    }

    private void DesintegrateObjects()
    {
        desintegrateableObjects = new List<GameObject>(GameObject.FindGameObjectsWithTag("DesintegrateObject"));

        foreach (GameObject desOBJ in desintegrateableObjects)
        {
            EggShield eggShield = desOBJ.GetComponentInChildren<EggShield>();

            if (eggShield != null)
            {
                eggShield.Desintegrate();
            }
        }
    }

    private void DestroyDesintegrateObjects()
    {
        foreach (GameObject desOBJ in desintegrateableObjects)
        {
            if (desOBJ != null)
            {
                Destroy(desOBJ);
            }
        }
    }

    private void ChangeObjetiveToWinCollider()
    {
        winCollider.SetActive(true);
    }

    private void LoseLevel()
    {
        isLevelLost = true;

        Time.timeScale = 0.0f;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;

        OnDefeat?.Invoke();
        gameEndScreenUI.PlayMissionFailedAnimation();
    }

    private void WinLevel()
    {
        Time.timeScale = 0.0f;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;

        OnWin.Invoke();

        gameEndScreenUI.PlayMissionAccomplishedAnimation();

        gamePlayCanvas.gameObject.SetActive(false);
        gamePlayVFXCanvas.gameObject.SetActive(false);
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
            
            AkUnitySoundEngine.SetState("Gameplay_Pause", "Paused");
            AkUnitySoundEngine.PostEvent("Pause_SFX", gameObject);
        }
        else
        {
            Time.timeScale = 1f;
            OnGameUnpaused?.Invoke();
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            AkUnitySoundEngine.SetState("Gameplay_Pause", "Unpaused");
            AkUnitySoundEngine.PostEvent("Resume_SFX", gameObject);
        }
    }
}
