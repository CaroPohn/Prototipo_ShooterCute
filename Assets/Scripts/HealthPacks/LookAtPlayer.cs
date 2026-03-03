using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    public Transform player;   // Asigná acá tu personaje desde el Inspector

    void Update()
    {
        if (player == null) return;

            transform.LookAt(player);
    }
}
