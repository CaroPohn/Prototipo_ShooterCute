using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SummaryUI : SpaceShipZoneScreen
{
    [SerializeField] GameObject[] lummingsWeapon;
    [SerializeField] GameObject[] lummingsAbility;
    [SerializeField] TextMeshProUGUI weaponTMP;
    [SerializeField] TextMeshProUGUI abilityTMP;
    [SerializeField] string[] lummingsNames;
    [SerializeField] Ready_Button_Spaceship readyButton;
    [SerializeField] CameraChanger changer;
    [SerializeField] GameObject container;

    private string weaponName;
    private string abilityName;

    public string sceneName;

    [SerializeField] private LoadoutUI loadout;

    public override void ShowAllScreens()
    {
        container.SetActive(true);
        readyButton.PlayAnimationReady();
    }

    public override void HideAllScreens()
    {
        container.SetActive(false);
    }

    public void UpdateLoadout(Lumming weapon,Lumming ability)
    {
        foreach (GameObject lumming in lummingsWeapon)
        {
            lumming.SetActive(false);
        }
        foreach (GameObject lumming in lummingsAbility)
        {
            lumming.SetActive(false);
        }
        lummingsWeapon[(int)weapon - 1].SetActive(true);
        lummingsAbility[(int)ability - 1].SetActive(true);
        weaponTMP.text = lummingsNames[(int)weapon - 1];
        abilityTMP.text = lummingsNames[(int)ability - 1];
    }

    public void StartLevel()
    {
        readyButton.PlayAnimationDisabled();
        changer.TransitionToCamera(3, 1);

        AkUnitySoundEngine.PostEvent("UI_Button_Special", gameObject);

        Invoke(nameof(StartLevelAfterCameraMoves), 0.1f);
    }

    private void StartLevelAfterCameraMoves()
    {
        if (loadout.savedWeaponLumming == Lumming.Bomb)
        {
            weaponName = "FireGun";
        }
        if (loadout.savedAbilityLumming == Lumming.Bomb)
        {
            abilityName = "BombAbility";
        }

        if (loadout.savedWeaponLumming == Lumming.Chispean)
        {
            weaponName = "ZapGun";
        }
        if (loadout.savedAbilityLumming == Lumming.Chispean)
        {
            abilityName = "ElectricAbility";
        }
        
        if (loadout.savedWeaponLumming == Lumming.Shotgun) 
        {
            weaponName = "Shotgun";
        }
        if (loadout.savedAbilityLumming == Lumming.Shotgun)
        {
            abilityName = "ShotgunAbility";
        }

        if (loadout.savedWeaponLumming == Lumming.Ice)
        {
            weaponName = "FreezeGun";
        }
        if (loadout.savedAbilityLumming == Lumming.Ice)
        {
            abilityName = "FreezeAbility";
        }

        PlayerSelectionData.selectedWeapon = weaponName;
        PlayerSelectionData.selectedAbility = abilityName;

        AkUnitySoundEngine.PostEvent("TransitionTo_Level", gameObject);

        SceneLoader.Instance.ChangeScene(sceneName);
    }
}
