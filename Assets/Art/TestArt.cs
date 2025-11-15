using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class TestArt : MonoBehaviour
{
    float previousHP = 1;
    [SerializeField] HealthBarUI healthBarUI;
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
        
    }
}
