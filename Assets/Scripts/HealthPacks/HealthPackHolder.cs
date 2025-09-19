using System.Collections;
using UnityEngine;

public class HealthPackHolder : MonoBehaviour
{
    private float timer;
    public float cooldown;

    private bool hasHealthPackBeenGrabbed;

    [SerializeField] private GameObject healthPack;
    [SerializeField] private GameObject cooldownEffectGO;
    [SerializeField] private Material cooldownEffectMat;
    [SerializeField] private HealthPackSystem healthPackSystem;

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

        transform.LookAt(player.transform);
    }

    private void GrabHealthPack()
    {
        healthPack.SetActive(false);
        cooldownEffectGO.SetActive(true);
        StartCoroutine(EffectCooldown());

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

    private IEnumerator EffectCooldown()
    {
        float startValue = 0.0f;
        float endValue = 1.0f;
        float duration = cooldown;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float currentValue = Mathf.Lerp(startValue, endValue, elapsed / duration);
            cooldownEffectMat.SetFloat("_Cooldown", currentValue);
            yield return null;
        }

        cooldownEffectMat.SetFloat("_Cooldown", endValue);
    }
}
