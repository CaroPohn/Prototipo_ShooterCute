using UnityEngine;

public class HealthPackHolder : MonoBehaviour
{
    private float timer;
    public float cooldown;

    private bool hasHealthPackBeenGrabbed;

    [SerializeField] private GameObject healthPack;

    private void OnEnable()
    {
        HealthPackSystem.OnGrabingHealthPack += GrabHealthPack;
        HealthPackSystem.OnGrabingHealthPack += ChangeGrabbedBool;
    }

    private void OnDisable()
    {
        HealthPackSystem.OnGrabingHealthPack -= GrabHealthPack;
        HealthPackSystem.OnGrabingHealthPack -= ChangeGrabbedBool;
    }

    private void Start()
    {
        timer = 0;
        hasHealthPackBeenGrabbed = false;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (hasHealthPackBeenGrabbed) 
        { 
            ReactivateHealthPack();
        }
    }

    private void GrabHealthPack()
    {
        healthPack.SetActive(false);
        timer = 0;
    }

    private void ReactivateHealthPack()
    {
        if (timer > cooldown) 
        {
            ChangeGrabbedBool();
            healthPack.SetActive(true);
        }
    }

    private void ChangeGrabbedBool()
    {
        hasHealthPackBeenGrabbed = !hasHealthPackBeenGrabbed;
    }
}
