using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class PlayerHealthSystem : MonoBehaviour
{
    [SerializeField] private Material lavaDamageEffect;
    [SerializeField] private Material projectileDamageEffect;

    public enum DamageType
    { 
        None,
        Lava,
        Projectile
    }

    public float maxHealth;
    public float health;

    private DamageType damageType;

    [SerializeField] private Image healthBarImage;

    public Action onDeath;

    private void Start()
    {
        health = maxHealth;
    }

    private void OnEnable()
    {
        DeathMapLimit.OnExitingLava += ChangeLavaCameraEffect;
    }

    private void OnDisable()
    {
        DeathMapLimit.OnExitingLava -= ChangeLavaCameraEffect;
    }

    private void Update()
    {
        UpdateHealthBar();
        ManageScreenDamageEffect();
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

    public void SetDamageType(DamageType damageTypeToActivate)
    {
        damageType = damageTypeToActivate;
    }

    private void ManageScreenDamageEffect()
    {
        if (damageType == DamageType.None)
        {
            lavaDamageEffect.SetFloat("_Intensity", 0);
            projectileDamageEffect.SetFloat("_Intensity", 0);
        }
        else if (damageType == DamageType.Lava)
        {
            lavaDamageEffect.SetFloat("_Intensity", 1);
        }
        else if (damageType == DamageType.Projectile)
        {
            projectileDamageEffect.SetFloat("_Intensity", 1);
            StartCoroutine(EffectCooldown());
        }
    }

    private void ChangeLavaCameraEffect()
    {
        damageType = DamageType.None;
    }    

    private IEnumerator EffectCooldown()
    {
        yield return new WaitForSeconds(1f);
        damageType = DamageType.None;
    }
}
