using UnityEngine;

[RequireComponent (typeof(InputReader))]
public class GameManager : MonoBehaviour
{
    private InputReader inputReader = null;

    //Dos instancias de bichitos
    //Una instancia de la gun

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
        Debug.Log("Caro es una gran mujer");
    }
}
