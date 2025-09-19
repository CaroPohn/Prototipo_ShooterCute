using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class PlayerHealthSystem : MonoBehaviour
{
    [SerializeField] private Material lavaDamageEffect;
    [SerializeField] private Material projectileDamageEffect;
    [SerializeField] private Material healthEffect;

    private float damage = 0.2f;
    private float postExitDuration = 3f;
    private float damageInterval = 0.1f;

    private float timeElapsedSinceExit;

    public enum EffectType
    {
        None,
        Lava,
        Projectile,
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
    }

    private void OnEnable()
    {
        DeathMapLimit.OnExitLava += StartLavaExitDamageCorroutine;
    }

    private void OnDisable()
    {
        DeathMapLimit.OnExitLava -= StartLavaExitDamageCorroutine;
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

            playerDeath();
        }
    }

    private void playerDeath()
    {
        gameObject.SetActive(false);
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
            lavaDamageEffect.SetFloat("_Intensity", 0);
            projectileDamageEffect.SetFloat("_Intensity", 0);
            healthEffect.SetFloat("_Intensity", 0);
        }
        else if (effectType == EffectType.Lava)
        {
            lavaDamageEffect.SetFloat("_Intensity", 1);
        }
        else if (effectType == EffectType.Projectile)
        {
            projectileDamageEffect.SetFloat("_Intensity", 1);
            StartCoroutine(EffectCooldown());
        }
        else if (effectType == EffectType.Heal)
        {
            lavaDamageEffect.SetFloat("_Intensity", 0);
            healthEffect.SetFloat("_Intensity", 1);
            StartCoroutine(HealEffectCooldown());

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
            elapsed += Time.deltaTime;
            float currentValue = Mathf.Lerp(startValue, endValue, elapsed / duration);
            healthEffect.SetFloat("_Intensity", currentValue);
            yield return null;
        }

        healthEffect.SetFloat("_Intensity", endValue);
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
