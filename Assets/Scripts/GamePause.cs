using UnityEngine;
using UnityEngine.UI;

public class GamePause : MonoBehaviour
{
    [SerializeField] private InputReader inputReader;

    [SerializeField] private Button restartButton;

    [SerializeField] private Canvas settingsCanvas;
    [SerializeField] private Canvas pauseCanvas;

    private void Start()
    {
        LevelController.OnGamePaused += ShowPause;
        LevelController.OnGameUnpaused += HidePause;

        settingsCanvas.gameObject.SetActive(false);
        pauseCanvas.gameObject.SetActive(false);

        restartButton.onClick.AddListener(Restart);

        HidePause();
    }

    private void OnDestroy()
    {
        LevelController.OnGamePaused -= ShowPause;
        LevelController.OnGameUnpaused -= HidePause;
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

    void HidePause()
    {
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
}
