using UnityEngine;

public class CreditsManager : MonoBehaviour
{
    public void GoToMainMenuScene()
    {
        SceneLoader.Instance.ChangeScene("Main_Menu_LavaWorld");
    }
}
