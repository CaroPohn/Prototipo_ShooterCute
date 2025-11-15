using System.Collections;
using System.Timers;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField] private Image hpImage;
    [SerializeField] private Image damagedRecievedBar;
    [Range(0f, 100f)][SerializeField] private float hpPercentageLostPerSecond;
    [SerializeField] private Color healingColor;
    [SerializeField] private float healingGlowDuration;
    [SerializeField] private Color damageColor;
    [SerializeField] private float DamageGlowDuration;
    [SerializeField] private AnimationCurve intensityDropAnimCurve;
    private float damagedRecievedBarCurrentFill;
    private float currentHP;
    Coroutine laggedHPBarCoroutine;
    Coroutine GlowAnimationCoroutine;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        damagedRecievedBarCurrentFill = 1;
        currentHP = 1;

        hpImage.material = Instantiate(hpImage.material);
        damagedRecievedBar.material = Instantiate(damagedRecievedBar.material);

    }
    /// <summary>
    ///Accepts values from 0 to 1, 1 Indicanting full HP
    /// </summary>

    public void UpdateHPBar(float percentage)
    {
        percentage = Mathf.Clamp01(percentage);
        SetNewHPBarValue(hpImage, percentage);
        float previousHP = currentHP;
        currentHP = percentage;
        if (percentage > previousHP) 
        {
            GainedHP();
        }
        else if(percentage < previousHP)
        {
            LostHP();
        }
    }

    void GainedHP()
    {
        if (currentHP > damagedRecievedBarCurrentFill) damagedRecievedBarCurrentFill = currentHP;
        GainedHPGlowAnimation();
    }
    void LostHP()
    {
        if(laggedHPBarCoroutine == null)
        {
            laggedHPBarCoroutine = StartCoroutine("StartLosingHPCourutine");
        }
        LostHPGlowAnimation();
    }
    void GainedHPGlowAnimation()
    {
        if (GlowAnimationCoroutine != null) 
        {
            StopCoroutine(GlowAnimationCoroutine);
            GlowAnimationCoroutine = null;
        }
        GlowAnimationCoroutine = StartCoroutine(HPAnimationCourutine(healingColor, healingGlowDuration));
    }
    void LostHPGlowAnimation()
    {
        if (GlowAnimationCoroutine != null)
        {
            StopCoroutine(GlowAnimationCoroutine);
            GlowAnimationCoroutine = null;
        }
        GlowAnimationCoroutine = StartCoroutine(HPAnimationCourutine(damageColor, DamageGlowDuration));

    }
    IEnumerator HPAnimationCourutine(Color glowColor,float effectDuration)
    {
        hpImage.material.SetColor("_GlowColor", glowColor);
        float initialGlow = 0.5f;
        hpImage.material.SetFloat("_GlowIntensity", initialGlow);
        float glowProgress = 0;
        while (glowProgress < effectDuration)
        {
            glowProgress += Time.deltaTime;
            if (glowProgress >= effectDuration) break;
            hpImage.material.SetFloat("_GlowIntensity", intensityDropAnimCurve.Evaluate(glowProgress / effectDuration) * initialGlow);
            yield return null;
        }
        hpImage.material.SetFloat("_GlowIntensity", 0);
    }
    IEnumerator StartLosingHPCourutine()
    {
        while(damagedRecievedBarCurrentFill > currentHP)
        {
            float hpToLose = Time.deltaTime * (hpPercentageLostPerSecond/10f);
            damagedRecievedBarCurrentFill -= hpToLose;
            if (damagedRecievedBarCurrentFill < currentHP) break;
            SetNewHPBarValue(damagedRecievedBar, damagedRecievedBarCurrentFill);
            yield return null;
        }
        SetNewHPBarValue(damagedRecievedBar, currentHP);
        laggedHPBarCoroutine = null;
    }
    void SetNewHPBarValue(Image hpBarImage,float value)
    {
        hpBarImage.material.SetFloat("_Fade", value);
    }
}
