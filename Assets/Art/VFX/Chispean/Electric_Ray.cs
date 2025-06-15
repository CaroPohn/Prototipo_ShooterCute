using UnityEngine;
using UnityEngine.VFX;

public class Electric_Ray : MonoBehaviour
{
    [SerializeField] VisualEffect rayEffect;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shoot()
    {
        PlayRay();
    }

    void PlayRay()
    {
        rayEffect.SendEvent("PlayRay");
    }

    
}
