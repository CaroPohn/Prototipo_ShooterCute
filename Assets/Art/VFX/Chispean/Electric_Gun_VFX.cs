using UnityEngine;
using UnityEngine.VFX;

public class Electric_Gun_VFX : MonoBehaviour
{
    [SerializeField] VisualEffect rayEffect;
    //[SerializeField] VisualEffect muzzle;
    [SerializeField] GameObject chargeObject;

    static readonly int distancePropID = Shader.PropertyToID("Hit distance");

    public void Charge()
    {
        chargeObject.SetActive(true);
        //muzzle.SendEvent("StartMuzzle");
    }

    public void Release()
    {
        //muzzle.Stop();
        chargeObject.SetActive(false);
        PlayRay(36f,0.5f, transform);
    }

    public void Release(float hitDistance,float intensity, Transform pivot)
    {
        //muzzle.Stop();
        chargeObject.SetActive(false);
        PlayRay(hitDistance, intensity, pivot);

    }
    public void Cancel()
    {
        //muzzle.Stop();
        chargeObject.SetActive(false);

    }

    void PlayRay(float hitDistance, float intensity, Transform pivot)
    {
        VisualEffect rayInstance = Instantiate(rayEffect, pivot.transform.position, pivot.transform.rotation);

        rayInstance.SendEvent("PlayRay");
        rayInstance.SetFloat(distancePropID, hitDistance);
        rayInstance.SetFloat("Intensity", intensity);
    }
}
