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
                // Cambió de quemado a no quemado
                AkUnitySoundEngine.SetState("Player_Burn", "Not_Burning");
                AkUnitySoundEngine.PostEvent("Player_Burn", gameObject); // si tenés este evento en Wwise
                isCurrentlyBurning = false;
            }
        }
        else if (effectType == EffectType.Lava)
        {
            lavaMat.SetFloat("_Intensity", 1);
            StopCoroutine(HealEffectCooldown());

            if (!isCurrentlyBurning)
            {
                // Entró en estado de quemado
                AkUnitySoundEngine.SetState("Player_Burn", "Burning");
                AkUnitySoundEngine.PostEvent("Player_Burn", gameObject);
                isCurrentlyBurning = true;
            }
        }
        else if (effectType == EffectType.EnemyDamage)
        {
            damageMat.SetFloat("_Intensity", 1);
            StartCoroutine(EffectCooldown());
        }
        else if (effectType == EffectType.Heal)
        {
            lavaMat.SetFloat("_Intensity", 0);
            healthMat.SetFloat("_Intensity", 1);
            StartCoroutine(HealEffectCooldown());

            if (isCurrentlyBurning)
            {
                AkUnitySoundEngine.SetState("Player_Burn", "None");
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

    private IEnumerator EffectCooldown()
    {
        yield return new WaitForSeconds(1f);
        effectType = EffectType.None;
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
