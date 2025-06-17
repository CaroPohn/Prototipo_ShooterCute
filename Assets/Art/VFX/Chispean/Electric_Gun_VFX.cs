using UnityEngine;
using UnityEngine.VFX;

public class Electric_Gun_VFX : MonoBehaviour
{
    [SerializeField] VisualEffect rayEffect;
    //[SerializeField] VisualEffect muzzle;
    [SerializeField] GameObject chargeObject;

    public void Charge()
    {
        chargeObject.SetActive(true);
        //muzzle.SendEvent("StartMuzzle");
    }

    public void Release()
    {
        //muzzle.Stop();
        chargeObject.SetActive(false);
        PlayRay(36f,0.5f);
    }

    public void Release(float hitDistance,float intensity)
    {
        //muzzle.Stop();
        chargeObject.SetActive(false);
        PlayRay(hitDistance,intensity);
    }
    public void Cancel()
    {
        //muzzle.Stop();
        chargeObject.SetActive(false);

    }

    void PlayRay(float hitDistance, float intensity)
    {
        rayEffect.SendEvent("PlayRay");
        rayEffect.SetFloat("Hit distance", hitDistance);
        rayEffect.SetFloat("Intensity", intensity);
    }
}
