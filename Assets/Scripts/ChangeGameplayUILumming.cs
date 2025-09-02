using UnityEngine;
using UnityEngine.UI;

public class ChangeGameplayUILumming : MonoBehaviour
{
    [SerializeField] private PlayerWeaponChoose playerWeaponChooseScript;

    [SerializeField] private Image lummingGameplayUI;

    [SerializeField] private Sprite fireLumming;
    [SerializeField] private Sprite zapLumming;

    private void Update()
    {
        if (PlayerSelectionData.selectedAbility == "FireGun")
        {
            lummingGameplayUI.sprite = fireLumming;
        }
        else if (PlayerSelectionData.selectedAbility == "ZapGun")
        {
            lummingGameplayUI.sprite = zapLumming;
        }
        else
        {
            lummingGameplayUI.sprite = zapLumming;
        }
    }
}
