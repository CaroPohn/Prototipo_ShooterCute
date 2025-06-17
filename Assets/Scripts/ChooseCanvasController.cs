using UnityEngine;

public class ChooseCanvasController : MonoBehaviour
{
    [SerializeField] private GameObject fireGunButton;
    [SerializeField] private GameObject electricGunButton;

    [SerializeField] private GameObject fireAbilityButton;
    [SerializeField] private GameObject electricAbilityButton;

    private void Start()
    {
        fireAbilityButton.SetActive(false);
        electricAbilityButton.SetActive(false);
    }

    private void OnEnable()
    {
        PlayerWeaponChoose.OnGunSelected += DeactivateGunButtons;
        PlayerWeaponChoose.OnGunSelected += ActivateAbilityButtons;
    }

    private void OnDisable()
    {
        PlayerWeaponChoose.OnGunSelected -= DeactivateGunButtons;
        PlayerWeaponChoose.OnGunSelected -= ActivateAbilityButtons;
    }

    private void DeactivateGunButtons()
    {
        fireGunButton.SetActive(false);
        electricGunButton.SetActive(false);
    }

    private void ActivateAbilityButtons()
    {
        fireAbilityButton.SetActive(true);
        electricAbilityButton.SetActive(true);
    }
}
