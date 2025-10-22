using UnityEngine;

public class EnemySoul : MonoBehaviour
{
    [SerializeField] ParticleSystem ps;
    int previousAmountOfParticles = 0;

    public delegate void DeathAction();
    public static event DeathAction OnParticleDeath;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ps.trigger.SetCollider(0,GameObject.Find("Egg Soul Force Field").GetComponent<Collider>());
        ps.Play();
        AkUnitySoundEngine.PostEvent("Projectile_Soul_Deploy", gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        int currentParticles = ps.particleCount;
        if (previousAmountOfParticles > currentParticles)
        {
            KillSoul();
        }
        else previousAmountOfParticles = currentParticles;
    }
    void KillSoul()
    {
        //Debug.Log("Particle killed!");
        if(OnParticleDeath != null) OnParticleDeath();
        Destroy(this.gameObject);
    }
}
