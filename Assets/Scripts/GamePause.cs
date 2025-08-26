using UnityEngine;
using UnityEngine.UI;

public class GamePause : MonoBehaviour
{
    [SerializeField] private InputReader inputReader;

    [SerializeField] private Button restartButton;

    private void Start()
    {
        LevelController.OnGamePaused += Show;
        LevelController.OnGameUnpaused += Hide;

        restartButton.onClick.AddListener(Restart);

        Hide();
    }

    private void OnDestroy()
    {
        LevelController.OnGamePaused -= Show;
        LevelController.OnGameUnpaused -= Hide;
    }

    void Hide()
    {
        gameObject.SetActive(false);
    }

    void Show()
    {
        gameObject.SetActive(true);
    }

    void Restart()
    {
        SceneLoader.Instance.ChangeScene("SelectionMenu");
    }
}
