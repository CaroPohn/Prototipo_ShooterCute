using UnityEngine;

public class BombHolder : MonoBehaviour
{
    [SerializeField] private GameObject bomb;
    [SerializeField] private GameObject electric;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Instantiate(bomb, transform.position, Quaternion.identity);
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            Instantiate(electric, transform.position, Quaternion.identity);
        }
    }
}
