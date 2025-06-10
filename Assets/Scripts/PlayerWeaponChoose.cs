using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerWeaponChoose : MonoBehaviour
{
    [SerializeField] private Canvas chooseCanvas;

    public static event Action OnGunSelected;

    public bool playerChooseFireGun;
    public bool playerChooseZapGun;

    [SerializeField] private Button fireGunButton;
    [SerializeField] private Button electricGunButton;

    void Start()
    {
        playerChooseFireGun = false;
        playerChooseZapGun = false;    
    }

    public void FireGunOption()
    {
        playerChooseFireGun = true;

        OnGunSelected?.Invoke();
    }

    public void ElectricGunOption() 
    { 
        playerChooseZapGun = true;

        OnGunSelected?.Invoke();
    }
}
