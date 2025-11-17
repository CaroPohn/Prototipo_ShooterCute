using UnityEngine;


public class HealthPackHolder : MonoBehaviour
{
    private float timer;
    public float cooldown;

    private bool hasHealthPackBeenGrabbed;

    [SerializeField] private GameObject healthPack;
    [SerializeField] private GameObject cooldownEffectGO;

    [SerializeField] private HealthPackSystem healthPackSystem;

    [SerializeField] private Transform modelTransform;

    [SerializeField] private GameObject player;
 
    private void OnEnable()
    {
        healthPackSystem.OnGrabingHealthPack += GrabHealthPack;
        healthPackSystem.OnGrabingHealthPack += ChangeGrabbedBool;
    }

    private void OnDisable()
    {
        healthPackSystem.OnGrabingHealthPack -= GrabHealthPack;
        healthPackSystem.OnGrabingHealthPack -= ChangeGrabbedBool;
    }

    private void Start()
    {
        timer = 0;
        hasHealthPackBeenGrabbed = false;
        cooldownEffectGO.SetActive(false);
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (hasHealthPackBeenGrabbed) 
        { 
            ReactivateHealthPack();
        }

        modelTransform.LookAt(player.transform);
    }

    private void GrabHealthPack()
    {
        healthPack.SetActive(false);
        cooldownEffectGO.SetActive(true);

        timer = 0;
    }

    private void ReactivateHealthPack()
    {
        if (timer > cooldown) 
        {
            ChangeGrabbedBool();
            healthPack.SetActive(true);
            cooldownEffectGO.SetActive(false);
        }
    }

    private void ChangeGrabbedBool()
    {
        hasHealthPackBeenGrabbed = !hasHealthPackBeenGrabbed;
    }
}
