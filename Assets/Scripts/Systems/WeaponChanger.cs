using System;
using System.Net;
using UnityEngine;
using UnityEngine.UI;

public class WeaponChanger : MonoBehaviour
{
    [SerializeField] private GameObject bomb;
    [SerializeField] private GameObject electric;
    [SerializeField] private GameObject shotgunAbility;
    [SerializeField] private GameObject iceAbility;

    [SerializeField] private GameObject bombHolder;
    [SerializeField] private GameObject gunHandler;

    [SerializeField] public Image FillAbilityImage;

    public float timer;

    public int weaponIndex;

    private string weaponName;
    private string abilityName;

    [SerializeField] private PlayerWeaponChoose playerWeaponChooseScript;

    [SerializeField] private Animator abilityUIAnim;

    [SerializeField] private InputReader inputReader;

    [SerializeField] public Animator armsAnimator;
    [SerializeField] private Animator bombAnimator;
    [SerializeField] private Animator electricAnimator;
    [SerializeField] private Animator jhonnyAnimator;
    [SerializeField] private Animator iceAnimator;

    [SerializeField] private GameObject bombAbilityAnimGO;
    [SerializeField] private GameObject electricAbilityAnimGO;
    [SerializeField] private GameObject jhonnyAbilityAnimGO;
    [SerializeField] private GameObject iceAbilityAnimGO;

    [SerializeField] private ZapGun zapGunScript;

    private bool changeToAbility;

    public static event Action OnAbilitySelected;

    GameObject selectedGun;
    GameObject selectedAbility;

    private void Start()
    {
        weaponName = PlayerSelectionData.selectedWeapon;
        abilityName = PlayerSelectionData.selectedAbility;

        selectedGun = FindChildWithTag(gunHandler.transform, weaponName);
        selectedAbility = GetAbilityPrefabByTag(abilityName);

        changeToAbility = false;

        weaponIndex = 1;
        ChangeWeapon();
    }

    private void OnEnable()
    {
        inputReader.OnUseAbility += ChangeGunBool;
        //inputReader.OnChangeToWeapon += ChangeToWeapon;

        ArmsAnimatorHandler.OnThrowToIdle += ActivateGun;
    }

    private void OnDisable()
    {
        inputReader.OnUseAbility -= ChangeGunBool;
        //inputReader.OnChangeToWeapon -= ChangeToWeapon;

        ArmsAnimatorHandler.OnThrowToIdle -= ActivateGun;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (FillAbilityImage.fillAmount <= 1)
        {
            FillAbilityImage.fillAmount += (1f / 10f) * Time.deltaTime;
        }

        if (timer >= 10.0f)
        {
            abilityUIAnim.SetBool("isReady", true);
        }
        else
        {
            abilityUIAnim.SetBool("isReady", false);
        }
    }

    private void ChangeGunBool()
    {
        changeToAbility = !changeToAbility;

        SwapGuns();
    }

    private void SwapGuns()
    {
        if (changeToAbility == true)
        {
            ChangeToAbility();
        }
        else if (changeToAbility == false)
        {
            ChangeToWeapon();
        }
    }

    private void ActivateGun()
    {
        if (!selectedGun.activeSelf)
        {
            selectedGun.SetActive(true);
        }
    }

    private void ChangeToWeapon()
    {
        weaponIndex = 1;
        ChangeWeapon();
    }

    private void ChangeToAbility()
    {
        OnAbilitySelected?.Invoke();
        weaponIndex = 2;
        ChangeWeapon();

        if(abilityName == "ElectricAbility")
        {
            AkUnitySoundEngine.PostEvent("Lumming_Ability_Electric_Start", gameObject);
        }
        else if(abilityName == "BombAbility")
        {
            AkUnitySoundEngine.PostEvent("Lumming_Ability_Fire_Start", gameObject);
        }
    }

    public void ChangeWeapon()
    {
        if (weaponIndex == 1)
        {
            armsAnimator.SetBool("UsingAbility", false);

            TurnOffAbility();

            bombAbilityAnimGO.SetActive(false);
            electricAbilityAnimGO.SetActive(false);
            jhonnyAbilityAnimGO.SetActive(false);
            iceAbilityAnimGO.SetActive(false);
        }
        else if (weaponIndex == 2 && timer >= 10.0f && !zapGunScript.isHoldingShoot)
        {
            selectedGun.SetActive(false);
            
            if (abilityName == "BombAbility")
            {
                bombAbilityAnimGO.SetActive(true);

                armsAnimator.SetBool("UsingAbility", true);
                bombAnimator.SetTrigger("Ability");              
            }
            else if (abilityName == "ElectricAbility")
            {
                electricAbilityAnimGO.SetActive(true);

                armsAnimator.SetBool("UsingAbility", true);
                electricAnimator.SetTrigger("Ability");             
            }
            else if (abilityName == "ShotgunAbility")
            {
                jhonnyAbilityAnimGO.SetActive(true);

                armsAnimator.SetBool("UsingAbility", true);
                jhonnyAnimator.SetTrigger("Ability");
            }
            else if (abilityName == "FreezeAbility")
            {
                iceAbilityAnimGO.SetActive(true);

                armsAnimator.SetBool("UsingAbility", true);
                iceAnimator.SetTrigger("Ability");
            }

            if (selectedAbility != null)
            {
                selectedAbility.SetActive(true);
            }
        }
    }

    private void TurnOffAbility()
    {
        if (selectedAbility.activeSelf && timer >= 10.0f)
        {
            selectedAbility.SetActive(false);
            armsAnimator.SetTrigger("Ability_Cancel");

            if (selectedGun != null)
                selectedGun.SetActive(true);
        }

        if (selectedGun != null)
            selectedGun.SetActive(true);
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
        if (tag == "ShotgunAbility")
            return shotgunAbility;
        if (tag == "FreezeAbility")
            return iceAbility;

        return null;
    }
}

