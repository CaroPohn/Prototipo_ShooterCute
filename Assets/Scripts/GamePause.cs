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

    [SerializeField] private LevelController levelController;

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
        pauseCanvas.gameObject.SetActive(false);
        settingsCanvas.gameObject.SetActive(true);
    }

    public void HideSettingsCanvas()
    {
        pauseCanvas.gameObject.SetActive(true);
        settingsCanvas.gameObject.SetActive(false);
    }

    public void HidePause()
    {
        pauseCanvas.gameObject.SetActive(false);
        settingsCanvas.gameObject.SetActive(false);
    }

    public void HidePauseButton()
    {
        levelController.PauseGame();
        pauseCanvas.gameObject.SetActive(false);
        settingsCanvas.gameObject.SetActive(false);
    }

    void ShowPause()
    {
        pauseCanvas.gameObject.SetActive(true);
    }

    void Restart()
    {
        SceneLoader.Instance.ChangeScene("SelectionMenu");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
