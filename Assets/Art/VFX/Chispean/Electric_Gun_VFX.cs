using UnityEngine;
using UnityEngine.VFX;

public class Electric_Gun_VFX : MonoBehaviour
{
    [SerializeField] VisualEffect rayEffect;
    [SerializeField] VisualEffect muzzle;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //SimpleTestCase();
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
        muzzle.SendEvent("StartMuzzle");
    }
    /// <summary>
    /// Shoots default Ray
    /// </summary>
    public void Release()
    {
        muzzle.Stop();
        PlayRay(36f,0.5f);
    }
    /// <summary>
    /// intensity is a value from 0 to 1, in which 0 is a quick release, and 1 is after a long time holding the attack
    /// </summary>
    public void Release(float hitDistance,float intensity)
    {
        muzzle.Stop();
        PlayRay(hitDistance,intensity);
    }
    public void Cancel()
    {
        muzzle.Stop();
    }
    void PlayRay(float hitDistance, float intensity)
    {
        rayEffect.SendEvent("PlayRay");
        rayEffect.SetFloat("Hit distance", hitDistance);
        rayEffect.SetFloat("Intensity", intensity);
    }

    
}
