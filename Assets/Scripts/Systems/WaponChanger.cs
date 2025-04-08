using UnityEngine;

public class WaponChanger : MonoBehaviour 
{
    [SerializeField] GameObject Gun1;
    [SerializeField] GameObject Gun2;
    [SerializeField] GameObject Gun3;
    [SerializeField] GameObject Gun4;

    private int weaponIndex;

    private void Start()
    {
        weaponIndex = 1;
        ChangeWeapon(weaponIndex);
    }

    private void Update()
    {
        ChangeWeaponInput();
    }

    private void ChangeWeaponInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            weaponIndex = 1;
            ChangeWeapon(weaponIndex);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            weaponIndex = 2;
            ChangeWeapon(weaponIndex);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            weaponIndex = 3;
            ChangeWeapon(weaponIndex);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            weaponIndex = 4;
            ChangeWeapon(weaponIndex);
        }
    }

    private void ChangeWeapon(int weaponIndex)
    {
        if (weaponIndex == 1) 
        {
            Gun1.SetActive(true);
            Gun2.SetActive(false);
            Gun3.SetActive(false);
            if(Gun4 != null)
                Gun4.SetActive(false);
        } 
        else if (weaponIndex == 2) 
        {
            Gun1.SetActive(false);
            Gun2.SetActive(true);
            Gun3.SetActive(false);
            if (Gun4 != null)
                Gun4.SetActive(false);
        }
        else if (weaponIndex == 3)
        {
            Gun1.SetActive(false);
            Gun2.SetActive(false);
            Gun3.SetActive(true);
            if (Gun4 != null)
                Gun4.SetActive(false);
        }
        else if (weaponIndex == 4)
        {
            Gun1.SetActive(false);
            Gun2.SetActive(false);
            Gun3.SetActive(false);
            if (Gun4 != null)
                Gun4.SetActive(true);
        }
    }
}

