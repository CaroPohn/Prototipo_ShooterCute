using UnityEngine;
using UnityEngine.UI;

public class SummaryUI : MonoBehaviour
{
    [SerializeField] Image weaponImage;
    [SerializeField] Image abilityImage;
    [SerializeField] Sprite[] lummingsSprites;
    [SerializeField] Image planetImage;
    [SerializeField] Sprite[] planetsSprites;
    [SerializeField] GameObject leverCanvas;
    [SerializeField] CameraChanger changer;

    private string weaponName;
    private string abilityName;

    public string sceneName;

    [SerializeField] private LoadoutUI loadout;

    bool aLoadoutWasSelected = false;
    bool aPlanetWasSelected = false;

    public void UpdateLoadout(Lumming weapon,Lumming ability)
    {
        weaponImage.sprite = lummingsSprites[(int)weapon];
        abilityImage.sprite = lummingsSprites[(int)ability];
        aLoadoutWasSelected = true;
        TurnOnOffLever();
    }

    public void UpdatePlanet(World world)
    {
        aPlanetWasSelected = world != World.None;
        TurnOnOffLever();

    }

    public void TurnOnOffLever()
    {
        if(aLoadoutWasSelected && aPlanetWasSelected)
        {
            leverCanvas.SetActive(true);
        }
        else
        {
            leverCanvas.SetActive(false);
        }
    }

    public void StartLevel()
    {
        leverCanvas.SetActive(false);
        changer.ChangeCameraTo(SpaceshipZone.LookingAtDoor);

        AkUnitySoundEngine.PostEvent("UI_Button_Special", gameObject);

        Invoke(nameof(StartLevelAfterCameraMoves), 0.1f);
    }

    private void StartLevelAfterCameraMoves()
    {
        if (loadout.savedWeaponLumming == Lumming.Bomb)
        {
            weaponName = "FireGun";
        }

        if (loadout.savedAbilityLumming == Lumming.Chispean)
        {
            abilityName = "ElectricAbility";
        }

        if (loadout.savedWeaponLumming == Lumming.Chispean)
        {
            weaponName = "ZapGun";
        }

        if (loadout.savedAbilityLumming == Lumming.Bomb)
        {
            abilityName = "BombAbility";
        }

        PlayerSelectionData.selectedWeapon = weaponName;
        PlayerSelectionData.selectedAbility = abilityName;

        Debug.Log(PlayerSelectionData.selectedWeapon);
        Debug.Log(PlayerSelectionData.selectedAbility);

        SceneLoader.Instance.ChangeScene(sceneName);
    }
}
