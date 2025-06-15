using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerWeaponChoose : MonoBehaviour
{
    [SerializeField] private Canvas chooseCanvas;

    public static event Action OnGunSelected;
    public static event Action OnAbilitySelected;

    public bool playerChooseFireGun = false;
    public bool playerChooseZapGun = false;

    public bool playerChooseFireAbility = false;
    public bool playerChooseZapAbility = false;

    public string selectedWeaponName;
    public string selectedAbilityName;

    [SerializeField] private Button fireGunButton;
    [SerializeField] private Button electricGunButton;

    public void FireGunOption()
    {
        playerChooseFireGun = true;
        selectedWeaponName = "FireGun";

        OnGunSelected?.Invoke();
    }

    public void FireAbilityOption()
    {
        playerChooseFireAbility = true;
        selectedAbilityName = "BombAbility";

        OnAbilitySelected?.Invoke();
    }

    public void ElectricGunOption() 
    { 
        playerChooseZapGun = true;
        selectedWeaponName = "ZapGun";

        OnGunSelected?.Invoke();
    }

    public void ElectricAbilityOption()
    {
        playerChooseZapAbility = true;
        selectedAbilityName = "ElectricAbility";

        OnAbilitySelected?.Invoke();
    }
}
