using UnityEngine;
using UnityEngine.UI;

public class TestArt : MonoBehaviour
{
    [SerializeField] GameEndScreenUI gameEndScreenUI;

    private void Update()
    {
        

        if (Input.GetKeyUp(KeyCode.A))
        {
            gameEndScreenUI.PlayMissionAccomplishedAnimation();

        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            gameEndScreenUI.PlayMissionFailedAnimation();

        }
        
    }
}
