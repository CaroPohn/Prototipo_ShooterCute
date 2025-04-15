using UnityEngine;

public class BombHolder : MonoBehaviour
{
    [SerializeField] private GameObject bomb;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Instantiate(bomb, transform.position, Quaternion.identity);
        }
    }
}
