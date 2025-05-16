using UnityEngine;
public class GameManager : MonoBehaviour
{
    private InputReader inputReader = null;

    

    private void Awake()
    {
        inputReader = GetComponent<InputReader>();

        inputReader.OnShoot += Test;
    }

    private void OnDestroy()
    {
        inputReader.OnShoot -= Test;
    }

    private void Test()
    {

    }
}
