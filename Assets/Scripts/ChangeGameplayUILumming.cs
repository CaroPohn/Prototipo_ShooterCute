using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeGameplayUILumming : MonoBehaviour
{
    [SerializeField] private PlayerWeaponChoose playerWeaponChooseScript;

    [SerializeField] private Image lummingGameplayUI;

    [SerializeField] private Sprite fireLumming;
    [SerializeField] private Sprite zapLumming;
    [SerializeField] private Sprite shotgunLumming;
    [SerializeField] private Sprite iceLumming;

    private string abilityName;

    private void Start()
    {
        abilityName = PlayerSelectionData.selectedAbility;
    }

    //private void OnEnable()
    //{
    //    PlayerWeaponChoose.OnAbilitySelected += SetAbilityUI;
    //}

    //private void OnDisable()
    //{
    //    PlayerWeaponChoose.OnAbilitySelected -= SetAbilityUI;
    //}

    private void Update()
    {
        if (abilityName == "BombAbility")
        {
            lummingGameplayUI.sprite = fireLumming;
        }
        else if (abilityName == "ElectricAbility")
        {
            lummingGameplayUI.sprite = zapLumming;
        }
        else if(abilityName == "ShotgunAbility")
        {
            lummingGameplayUI.sprite = shotgunLumming;
        }
        else if(abilityName == "FreezeAbility")
        {
            lummingGameplayUI.sprite = iceLumming;
        }
    }
}
