using UnityEngine;
using UnityEngine.UI;

public class TestArt : MonoBehaviour
{
    [SerializeField] Animator animArms;
    [SerializeField] Animator animBomb;

    private void Update()
    {
        

        if (Input.GetKeyUp(KeyCode.A))
        {
            animArms.SetTrigger("Bomb_Ability");
            animBomb.SetTrigger("Ability");

        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            animArms.SetTrigger("Ability_Release");
            

        }
        
    }
}
