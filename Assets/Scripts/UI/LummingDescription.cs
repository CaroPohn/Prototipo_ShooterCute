using System;
using UnityEngine;
using TMPro;

public class LummingDescription : MonoBehaviour
{
    [SerializeField] LummingData[] lummingsData;
    [SerializeField] TextMeshProUGUI tmpLummingName;
    [SerializeField] TextMeshProUGUI tmpLummingWeaponDescription;
    [SerializeField] TextMeshProUGUI tmpLummingAbilityDescription;


    public void UpdateLummingDescription(Lumming lumming)
    {
        int index = (int)lumming;
        tmpLummingName.text = lummingsData[index].lummingName;
        tmpLummingWeaponDescription.text = lummingsData[index].weaponDescription;
        tmpLummingAbilityDescription.text = lummingsData[index].abilityDescription;
    }


}
[Serializable]
public struct LummingData
{
    public Lumming lumming;
    public string lummingName;
    public string weaponDescription;
    public string abilityDescription;
}
