using AbilityProperties;
using System;
using UnityEngine;

public class WeaponChanger : MonoBehaviour 
{
    [SerializeField] GameObject Gun1;
    [SerializeField] GameObject Gun2;

    [SerializeField] private GameObject bomb;
    [SerializeField] private GameObject electric;

    [SerializeField] private GameObject bombHolder;
    [SerializeField] private GameObject gunHandler;

    public float timer; 

    public int weaponIndex = 1;

    [SerializeField] private PlayerWeaponChoose playerWeaponChooseScript;

    [SerializeField] private InputReader inputReader;

    private void OnEnable()
    {
        inputReader.OnUseAbility += ChangeToAbility;
        inputReader.OnChangeToWeapon += ChangeToWeapon;
    }

    private void OnDisable()
    {
        inputReader.OnUseAbility -= ChangeToAbility;
        inputReader.OnChangeToWeapon -= ChangeToWeapon;
    }

    private void Update()
    {
        timer += Time.deltaTime;
    }

    private void ChangeToWeapon()
    {
        weaponIndex = 1;
        ChangeWeapon();
    }

    private void ChangeToAbility()
    {
        weaponIndex = 2;
        ChangeWeapon();
    }

    public void ChangeWeapon()
    {
        GameObject selectedGun = FindChildWithTag(gunHandler.transform, playerWeaponChooseScript.selectedWeaponName);
        GameObject selectedAbility = GetAbilityPrefabByTag(playerWeaponChooseScript.selectedAbilityName);

        if (weaponIndex == 1)
        {
            if (selectedGun != null)
                selectedGun.SetActive(true);
        }
        else if (weaponIndex == 2 && timer >= 10.0f)
        {
            selectedGun.SetActive(false);
        
            if (selectedAbility != null)
            {
                selectedAbility.SetActive(true);
            }
        }
    }

    GameObject FindChildWithTag(Transform parent, string tag)
    {
        foreach (Transform child in parent.transform)
        {
            if (child.CompareTag(tag))
            {
                return child.gameObject;
            }
        }

        return null;
    }

    GameObject GetAbilityPrefabByTag(string tag)
    {
        if (tag == "ElectricAbility")
            return electric;
        if (tag == "BombAbility")
            return bomb;

        return null;
    }
}

