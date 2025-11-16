using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class TestArt : MonoBehaviour
{
    float previousHP = 1;
    [SerializeField] HealthBarUI healthBarUI;
    [SerializeField] InteractHUD_UI interactHUD_UI;
    bool isVisible = false;
    private void Update()
    {
        

        if (Input.GetKeyUp(KeyCode.A))
        {
            previousHP -= 0.45f;
            healthBarUI.UpdateHPBar(previousHP); 
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {

            previousHP += 0.2f;
            healthBarUI.UpdateHPBar(previousHP);

        }
        if (Input.GetKeyUp(KeyCode.E))
        {
            
            if (isVisible)
            {
                interactHUD_UI.Hide();
            }
            else interactHUD_UI.Appear();
            isVisible = !isVisible;
        }

    }
}
