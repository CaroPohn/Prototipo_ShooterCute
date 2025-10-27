using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class PlayerHealthSystem : MonoBehaviour
{
    [SerializeField] private Material lavaDamageEffect;
    [SerializeField] private Material projectileDamageEffect;
    [SerializeField] private Material healthEffect;

    [SerializeField] private Transform spawnPoint;

    private float damage = 0.2f;
    private float postExitDuration = 3f;
    private float damageInterval = 0.1f;

    private float timeElapsedSinceExit;

    private bool isCurrentlyBurning;
    private bool hasPlayedDamageSound;
    private bool hasPlayedHealSound;

    private Material lavaMat;
    private Material healthMat;
    private Material damageMat;

    public enum EffectType
    {
        None,
        Lava,
        EnemyDamage,
        Heal
    }

    public float maxHealth;
    public float health;

    private EffectType effectType;

    [SerializeField] private Image healthBarImage;

    public Action onDeath;

    private void Start()
    {
        health = maxHealth;

        lavaMat = lavaDamageEffect;
        healthMat = healthEffect;
        damageMat = projectileDamageEffect;

        isCurrentlyBurning = false;
        hasPlayedDamageSound = false;
        hasPlayedHealSound = false;

        AkUnitySoundEngine.SetState("Player_Burn", "None");
    }

    private void OnEnable()
    {
        DeathMapLimit.OnExitLava += StartLavaExitDamageCorroutine;
        GamePause.OnRestartLevel += ResetPlayer;
    }

    private void OnDisable()
    {
        DeathMapLimit.OnExitLava -= StartLavaExitDamageCorroutine;
        GamePause.OnRestartLevel -= ResetPlayer;
    }

    private void Update()
    {
        UpdateHealthBar();
        ManageScreenDamageEffect();

        timeElapsedSinceExit += Time.deltaTime;
    }

    public void TakeDamage(float damage)
    {
        if (!gameObject.activeSelf)
        {
            return;
        }

        health -= damage;

        if (health <= 0)
        {
            health = 0;

            PlayerDeath();
        }
    }

    private void ResetPlayer()
    {
        healthMat.SetFloat("_Intensity", 0);
        effectType = EffectType.None;
    }

    private void PlayerDeath()
    {
        gameObject.SetActive(false);
        gameObject.transform.position = spawnPoint.position;
    }

    private void UpdateHealthBar()
    {
        healthBarImage.fillAmount = health / maxHealth;
    }

    public void SetEffectType(EffectType damageTypeToActivate)
    {
        effectType = damageTypeToActivate;
    }

    private void ManageScreenDamageEffect()
    {
        if (effectType == EffectType.None)
        {
            lavaMat.SetFloat("_Intensity", 0);
            damageMat.SetFloat("_Intensity", 0);
            healthMat.SetFloat("_Intensity", 0);
            if (isCurrentlyBurning)
            {
                AkUnitySoundEngine.SetSwitch("Player_Burn", "Not_Burning", gameObject);
                AkUnitySoundEngine.PostEvent("Player_Burn", gameObject);
                isCurrentlyBurning = false;
            }
        }
        else if (effectType == EffectType.Lava)
        {
            lavaMat.SetFloat("_Intensity", 1);
            StopCoroutine(HealEffectCooldown());

            if (!isCurrentlyBurning)
            {
                AkUnitySoundEngine.SetSwitch("Player_Burn", "Burning", gameObject);
                AkUnitySoundEngine.PostEvent("Player_Burn", gameObject);
                
                isCurrentlyBurning = true;
            }
        }
        else if (effectType == EffectType.EnemyDamage)
        {
            damageMat.SetFloat("_Intensity", 1);

            if (!hasPlayedDamageSound)
            {
                AkUnitySoundEngine.PostEvent("Player_TakeDamage_Generic", gameObject);
                hasPlayedDamageSound = true;
            }

            StartCoroutine(DamageEffectCooldown());
        }
        else if (effectType == EffectType.Heal)
        {
            lavaMat.SetFloat("_Intensity", 0);
            healthMat.SetFloat("_Intensity", 1);
            
            if (!hasPlayedHealSound)
            {
                AkUnitySoundEngine.PostEvent("Lumming_Heal", gameObject);
                hasPlayedHealSound = true;
            }

            StartCoroutine(HealEffectCooldown());

            if (isCurrentlyBurning)
            {
                AkUnitySoundEngine.SetSwitch("Player_Burn", "None", gameObject);
                AkUnitySoundEngine.PostEvent("Player_Burn", gameObject);
                isCurrentlyBurning = false;
            }

            StopCoroutine(LavaDamageOverTimeAfterExit());
        }

        
    }

    private void StartLavaExitDamageCorroutine()
    {
        StartCoroutine(LavaDamageOverTimeAfterExit());
    }

    private IEnumerator DamageEffectCooldown()
    {
        yield return new WaitForSeconds(0.5f);
        effectType = EffectType.None;
        hasPlayedDamageSound = false;
    }

    private IEnumerator HealEffectCooldown()
    {
        float startValue = 0.8f;
        float endValue = 0.0f;
        float duration = 3.0f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            if (effectType == EffectType.Lava)
            {
                yield break;
            }

            elapsed += Time.deltaTime;
            float currentValue = Mathf.Lerp(startValue, endValue, elapsed / duration);
            healthEffect.SetFloat("_Intensity", currentValue);
            yield return null;
        }

        healthMat.SetFloat("_Intensity", endValue);
        effectType = EffectType.None;
        hasPlayedHealSound = false;
    }

    private IEnumerator LavaDamageOverTimeAfterExit()
    {
        timeElapsedSinceExit = 0;

        while (timeElapsedSinceExit < postExitDuration)
        {
            if (effectType == EffectType.Heal)
            {
                yield break;
            }

            TakeDamage(damage);
            SetEffectType(EffectType.Lava);

            yield return new WaitForSeconds(damageInterval);
        }

        effectType = EffectType.None;
    }
}
