using System;
using UnityEngine;
using TMPro;

public class LummingDescription : MonoBehaviour
{
    [SerializeField] LummingData[] lummingsData;
    [SerializeField] TextMeshProUGUI tmpLummingName;
    [SerializeField] TextMeshProUGUI tmpLummingWeaponDescription;
    [SerializeField] TextMeshProUGUI tmpLummingAbilityDescription;
    [SerializeField] GameObject container;


    public void UpdateLummingDescription(Lumming lumming)
    {
        int index = (int)lumming;
        tmpLummingName.text = lummingsData[index].lummingName;
        tmpLummingWeaponDescription.text = lummingsData[index].weaponDescription;
        tmpLummingAbilityDescription.text = lummingsData[index].abilityDescription;
    }
    public void HideScreen()
    {
        container.SetActive(false);
    }
    public void ShowScreen()
    {
        container.SetActive(true);
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
