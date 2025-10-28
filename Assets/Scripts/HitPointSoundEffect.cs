using UnityEngine;

public class HitPointSoundEffect : MonoBehaviour
{
    void OnEnable()
    {
        AkUnitySoundEngine.PostEvent("Projectile_Hit_ElectricProjectile", gameObject);
    }
}
