using System.Globalization;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject menuCanvas;
    [SerializeField] private GameObject settingsCanvas;

    private string wiseEventName = "UI_Button_Normal";

    public void StartLevel()
    {
        AkUnitySoundEngine.PostEvent(wiseEventName, gameObject);
        SceneLoader.Instance.ChangeScene("Spaceship_Interior");  
    }

    public void OpenSettings()
    {
        AkUnitySoundEngine.PostEvent(wiseEventName, gameObject);
        settingsCanvas.SetActive(true);        
    }

    public void ExitGame()
    {
        AkUnitySoundEngine.PostEvent(wiseEventName, gameObject);
        Application.Quit();
    }

    public void CloseSettings()
    {
        AkUnitySoundEngine.PostEvent(wiseEventName, gameObject);
        settingsCanvas.SetActive(false);   
    }

    public void GoToCreditsScene()
    {
        AkUnitySoundEngine.PostEvent(wiseEventName, gameObject);
        SceneLoader.Instance.ChangeScene("Credits");
    }
}
