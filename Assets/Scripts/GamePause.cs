using System;
using Unity.VisualScripting.InputSystem;
using UnityEngine;
using UnityEngine.UI;

public class GamePause : MonoBehaviour
{
    [SerializeField] private InputReader inputReader;

    [SerializeField] private Button restartButton;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button exitButton;

    [SerializeField] private Canvas settingsCanvas;
    [SerializeField] private Canvas pauseCanvas;

    [SerializeField] private Material lavaDamageEffect;
    [SerializeField] private Material heatEffect;
    [SerializeField] private Material projectileDamageEffect;
    [SerializeField] private Material healthEffect;

    [SerializeField] private LevelController levelController;

    private string wiseEventName = "UI_Button_Normal";

    public static event Action OnRestartLevel;

    private void Start()
    {
        LevelController.OnGamePaused += ShowPause;
        LevelController.OnGameUnpaused += HidePause;

        settingsCanvas.gameObject.SetActive(false);
        pauseCanvas.gameObject.SetActive(false);

        restartButton.onClick.AddListener(Restart);
    }

    private void OnDestroy()
    {
        LevelController.OnGamePaused -= ShowPause;
        LevelController.OnGameUnpaused -= HidePause;

        restartButton.onClick.RemoveListener(Restart);
    }

    public void ShowSettingsCanvas()
    {
        AkUnitySoundEngine.PostEvent(wiseEventName, gameObject);

        pauseCanvas.gameObject.SetActive(false);
        settingsCanvas.gameObject.SetActive(true);
    }

    public void HideSettingsCanvas()
    {
        AkUnitySoundEngine.PostEvent(wiseEventName, gameObject);

        pauseCanvas.gameObject.SetActive(true);
        settingsCanvas.gameObject.SetActive(false);  
    }

    public void HidePause()
    {
        AkUnitySoundEngine.PostEvent(wiseEventName, gameObject);

        pauseCanvas.gameObject.SetActive(false);
        settingsCanvas.gameObject.SetActive(false);     
    }

    public void HidePauseButton()
    {
        AkUnitySoundEngine.PostEvent(wiseEventName, gameObject);

        levelController.PauseGame();
        pauseCanvas.gameObject.SetActive(false);
        settingsCanvas.gameObject.SetActive(false);       
    }

    public void ShowPause()
    {
        AkUnitySoundEngine.PostEvent(wiseEventName, gameObject);

        pauseCanvas.gameObject.SetActive(true);     
    }

    public void Restart()
    {
        OnRestartLevel?.Invoke();

        lavaDamageEffect.SetFloat("_Intensity", 0);
        heatEffect.SetFloat("_Intensity", 0);
        projectileDamageEffect.SetFloat("_Intensity", 0);
        healthEffect.SetFloat("_Intensity", 0);

        AkUnitySoundEngine.PostEvent("TransitionTo_Level", gameObject);

        SceneLoader.Instance.ChangeScene("LavaWorld-Map");
    }

    public void GoToShip()
    {
        OnRestartLevel?.Invoke();

        lavaDamageEffect.SetFloat("_Intensity", 0);
        heatEffect.SetFloat("_Intensity", 0);
        projectileDamageEffect.SetFloat("_Intensity", 0);
        healthEffect.SetFloat("_Intensity", 0);

        AkUnitySoundEngine.PostEvent("TransitionTo_Menu", gameObject);

        SceneLoader.Instance.ChangeScene("Spaceship_Interior");
    }

    public void ExitGame()
    {
        AkUnitySoundEngine.PostEvent(wiseEventName, gameObject);

        Application.Quit();
    }
}
