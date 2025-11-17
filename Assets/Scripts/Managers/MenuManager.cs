using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject menuCanvas;
    [SerializeField] private GameObject settingsCanvas;

    public void StartLevel()
    {
        SceneLoader.Instance.ChangeScene("Spaceship_Interior");
    }

    public void OpenSettings()
    {
        settingsCanvas.SetActive(true);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void CloseSettings()
    {
        settingsCanvas.SetActive(false);
    }
}
