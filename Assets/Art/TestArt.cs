using UnityEngine;
using UnityEngine.UI;

public class TestArt : MonoBehaviour
{
    [SerializeField] EggShield corruptedRoots;
    private void Update()
    {
        

        if (Input.GetKeyUp(KeyCode.A))
        {

            corruptedRoots.Desintegrate();
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            
            

        }
        
    }
}
