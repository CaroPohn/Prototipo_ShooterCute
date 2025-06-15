using System.Collections.Generic;
using UnityEngine;

public class GunHandler : MonoBehaviour
{
    [SerializeField] public List<GameObject> gunList;

    [SerializeField] private PlayerWeaponChoose playerWeaponChooseScript;

    private Transform gunHolder;

    private void Start()
    {
        gunHolder = GetComponent<Transform>();
    }

    private void OnEnable()
    {
        PlayerWeaponChoose.OnGunSelected += SetGunByPlayerChoose;
    }

    private void OnDisable()
    {
        PlayerWeaponChoose.OnGunSelected -= SetGunByPlayerChoose;
    }

    private void SetGunByPlayerChoose()
    {
        foreach (GameObject gun in gunList) 
        { 
            if (playerWeaponChooseScript.playerChooseZapGun && gun.TryGetComponent<ZapGun>(out ZapGun foundZapGun))
            {
                gun.SetActive(true);
                gun.transform.SetParent(gunHolder);
                gun.transform.localPosition = Vector3.zero;
                gun.transform.localRotation = Quaternion.identity;
            }

            if (playerWeaponChooseScript.playerChooseFireGun && gun.TryGetComponent<FireGun>(out FireGun foundFireGun))
            {
                gun.SetActive(true);
                gun.transform.SetParent(gunHolder);
                gun.transform.localPosition = Vector3.zero;
                gun.transform.localRotation = Quaternion.identity;
            }
        }
    }
}
