using UnityEngine;

public class CreditsManager : MonoBehaviour
{
    private string wiseEventName = "UI_Button_Normal";

    public void GoToMainMenuScene()
    {
        AkUnitySoundEngine.PostEvent(wiseEventName, gameObject);
        SceneLoader.Instance.ChangeScene("Main_Menu_LavaWorld");      
    }
}
