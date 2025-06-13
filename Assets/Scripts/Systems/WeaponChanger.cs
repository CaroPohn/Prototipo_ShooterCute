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

    //private void ChangeWeapon(int weaponIndex)
    //{
    //    if (playerWeaponChooseScript.playerChooseFireGun)
    //    {
    //        if (weaponIndex == 1)
    //        {
    //            //Gun1.SetActive(true);
    //            //Gun2.SetActive(false);

    //            Gun4 = FindChildWithTag(bombHolder, "Bomb");
    //            if (Gun4 != null)
    //                Gun4.SetActive(false);

    //            Gun5 = FindChildWithTag(bombHolder, "Electric");
    //            if (Gun5 != null)
    //                Gun5.SetActive(false);
    //        }
    //        else if (weaponIndex == 2 && timer >= 10.0f)
    //        {
    //            //Gun1.SetActive(false);
    //            //Gun2.SetActive(false);

    //            Gun5 = FindChildWithTag(bombHolder, "Electric");

    //            if (Gun5 != null)
    //            {
    //                Gun5.SetActive(true);
    //            }
    //            else if (Gun5 == null)
    //            {
    //                GameObject electricInstance = Instantiate(electric, bombHolder.position, Quaternion.identity);
    //                electricInstance.transform.SetParent(bombHolder.transform, true);
    //                Gun5 = FindChildWithTag(bombHolder, "Electric");
    //                Gun5.SetActive(true);
    //            }
    //        }
    //    }
    //    else if (playerWeaponChooseScript.playerChooseZapGun)
    //    {
    //        if (weaponIndex == 1)
    //        {
    //            //Gun1.SetActive(false);
    //            //Gun2.SetActive(true);

    //            Gun4 = FindChildWithTag(bombHolder, "Bomb");
    //            if (Gun4 != null)
    //                Gun4.SetActive(false);

    //            Gun5 = FindChildWithTag(bombHolder, "Electric");
    //            if (Gun5 != null)
    //                Gun5.SetActive(false);
    //        }
    //        else if (weaponIndex == 2 && timer >= 10.0f)
    //        {
    //            //Gun1.SetActive(false);
    //            //Gun2.SetActive(false);

    //            Gun4 = FindChildWithTag(bombHolder, "Bomb");

    //            if (Gun4 != null)
    //            {
    //                Gun4.SetActive(true);
    //            }
    //            else if (Gun4 == null)
    //            {
    //                GameObject bombInstance = Instantiate(bomb, bombHolder.position, Quaternion.identity);
    //                bombInstance.transform.SetParent(bombHolder.transform, true);
    //                Gun4 = FindChildWithTag(bombHolder, "Bomb");
    //                Gun4.SetActive(true);
    //            }   
    //        }
    //    }
        
    //}

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

