using UnityEngine;

public class TestArt : MonoBehaviour
{
    [SerializeField] EggShield eggShield;

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.A))
        {
            eggShield.Appear();
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            eggShield.Desintegrate();
        }
        else if (Input.GetKeyUp(KeyCode.G))
        {
            eggShield.GetHit();
        }
    }
}
