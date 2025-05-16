using UnityEngine;
using UnityEngine.UI;

public class PlayerWeaponChoose : MonoBehaviour
{
    [SerializeField] private Canvas chooseCanvas;

    public bool playerChooseFireGun;
    public bool playerChooseElectricGun;

    [SerializeField] private Button fireGunButton;
    [SerializeField] private Button electricGunButton;

    public bool playerHasChosen; 

    void Start()
    {
        playerChooseFireGun = false;
        playerChooseElectricGun = false;    
        playerHasChosen = false;
    }

    public void FireGunOption()
    {
        playerChooseFireGun = true;

        Debug.Log("Player choose fire weapon");
        playerHasChosen = true;
    }

    public void ElectricGunOption() 
    { 
        playerChooseElectricGun = true;

        Debug.Log("Player choose electric weapon");
        playerHasChosen = true;
    }
}
