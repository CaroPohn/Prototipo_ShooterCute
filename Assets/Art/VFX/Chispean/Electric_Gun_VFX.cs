using UnityEngine;
using UnityEngine.VFX;

public class Electric_Gun_VFX : MonoBehaviour
{
    [SerializeField] VisualEffect rayEffect;
    //[SerializeField] VisualEffect muzzle;
    [SerializeField] GameObject chargeObject;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SimpleTestCase();
    }
    void SimpleTestCase()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Charge();
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            Release();
        }
    }
    public void Charge()
    {
        chargeObject.SetActive(true);
        //muzzle.SendEvent("StartMuzzle");
    }
    /// <summary>
    /// Shoots default Ray
    /// </summary>
    public void Release()
    {
        //muzzle.Stop();
        chargeObject.SetActive(false);
        PlayRay(36f,0.5f);
    }
    /// <summary>
    /// intensity is a value from 0 to 1, in which 0 is a quick release, and 1 is after a long time holding the attack
    /// </summary>
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
