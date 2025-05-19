using UnityEngine;

public class FireGun : MonoBehaviour
{
    [SerializeField] private Transform shootPoint;
    [SerializeField] GameObject projectilePrefab;

    [SerializeField] private Camera playerCamera;
    
    public float damage;
    public float timeBetweenShots = 0.5f;

    [SerializeField] InputReader inputReader;

    private void OnEnable()
    {
        inputReader.OnShoot += Shoot;
    }

    private void OnDisable()
    {
        inputReader.OnShoot -= Shoot;
    }

    public void Shoot()
    {
        GameObject projectile = Instantiate(projectilePrefab, shootPoint.position, Quaternion.identity);

        Vector3 direction = playerCamera.transform.forward;

        Projectile projScript = projectile.GetComponent<Projectile>();
        if (projScript != null)
        {
            projScript.SetDamage(damage);
            projScript.SetDirection(direction);
        }
    }
}
