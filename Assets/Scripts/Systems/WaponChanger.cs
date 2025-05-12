using UnityEngine;

public class WaponChanger : MonoBehaviour 
{
    [SerializeField] GameObject Gun1;
    [SerializeField] GameObject Gun2;
    [SerializeField] GameObject Gun3;

    [SerializeField] private GameObject bomb;
    [SerializeField] private Transform bombHolder;

    private GameObject Gun4;

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
            //Gun1.SetActive(true);
            //Gun2.SetActive(false);
            //Gun3.SetActive(false);

            //Gun4 = FindChildWithTag(bombHolder, "Bomb");
            //if (Gun4 != null)
            //    Gun4.SetActive(false);
        } 
        else if (weaponIndex == 2) 
        {
            //Gun1.SetActive(false);
            //Gun2.SetActive(true);
            //Gun3.SetActive(false);

            //Gun4 = FindChildWithTag(bombHolder, "Bomb");
            //if (Gun4 != null)
            //    Gun4.SetActive(false);
        }
        else if (weaponIndex == 3)
        {
            //Gun1.SetActive(false);
            //Gun2.SetActive(false);
            //Gun3.SetActive(true);

            //Gun4 = FindChildWithTag(bombHolder, "Bomb");
            //if (Gun4 != null)
            //    Gun4.SetActive(false);
        }
        else if (weaponIndex == 4)
        {
            Gun1.SetActive(false);
            Gun2.SetActive(false);
            Gun3.SetActive(false);

            Gun4 = FindChildWithTag(bombHolder, "Bomb");

            if (Gun4 != null)
            {
                Gun4.SetActive(true);
            }
            else if(Gun4 == null)
            {
                Instantiate(bomb, bombHolder.position, Quaternion.identity, bombHolder);
                Gun4 = FindChildWithTag(bombHolder, "Bomb");
                Gun4.SetActive(true);
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
}

