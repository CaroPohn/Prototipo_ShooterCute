using System.Collections.Generic;
using UnityEngine;

public class AbilityHandler : MonoBehaviour
{
    [SerializeField] public List<GameObject> abiltyList;

    [SerializeField] private PlayerWeaponChoose playerWeaponChooseScript;

    private Transform abilityHolder;

    private void Start()
    {
        abilityHolder = GetComponent<Transform>();
    }

    private void OnEnable()
    {
        PlayerWeaponChoose.OnAbilitySelected += SetAbilityByPlayerChoose;
    }

    private void OnDisable()
    {
        PlayerWeaponChoose.OnAbilitySelected -= SetAbilityByPlayerChoose;
    }

    private void SetAbilityByPlayerChoose()
    {
        foreach (GameObject ability in abiltyList)
        {
            if (playerWeaponChooseScript.playerChooseZapAbility && ability.TryGetComponent<BombFriendSystem>(out BombFriendSystem foundBombFriendSystem))
            {
                //ability.transform.SetParent(abilityHolder);
                //ability.transform.localPosition = Vector3.zero;
                //ability.transform.localRotation = Quaternion.identity;
            }

            if (playerWeaponChooseScript.playerChooseFireAbility && ability.TryGetComponent<ElectricAbility>(out ElectricAbility foundElectricAbility))
            {
                //ability.transform.SetParent(abilityHolder);
                //ability.transform.localPosition = Vector3.zero;
                //ability.transform.localRotation = Quaternion.identity;
            }
        }
    }
}
