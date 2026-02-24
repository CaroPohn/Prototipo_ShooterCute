using UnityEngine;
using UnityEngine.UI;

public class SummaryUI : MonoBehaviour
{
    [SerializeField] Image weaponImage;
    [SerializeField] Image abilityImage;
    [SerializeField] Sprite[] lummingsSprites;
    [SerializeField] Image planetImage;
    [SerializeField] Sprite lavaWorldSprite;
    [SerializeField] GameObject missionDescriptionGO;
    [SerializeField] Ready_Button_Spaceship readyButton;
    [SerializeField] CameraChanger changer;
    [SerializeField] GameObject canvas;

    private string weaponName;
    private string abilityName;

    public string sceneName;

    [SerializeField] private LoadoutUI loadout;

    bool aLoadoutWasSelected = false;
    bool aPlanetWasSelected = false;

    public void DisplaySummaryCanvas()
    {
        canvas.SetActive(true);
        TurnOnOffLever();
    }

    public void LeaveReadyZone()
    {
        canvas.SetActive(false);
    }

    public void UpdateLoadout(Lumming weapon,Lumming ability)
    {
        weaponImage.sprite = lummingsSprites[(int)weapon];
        abilityImage.sprite = lummingsSprites[(int)ability];
        aLoadoutWasSelected = true;
        
    }

    public void UpdatePlanet(World world)
    {
        aPlanetWasSelected = (world != World.None);
        if(world  == World.Lava)
        {
            missionDescriptionGO.SetActive(true);
            planetImage.sprite = lavaWorldSprite;
        }

    }

    public void TurnOnOffLever()
    {
        if(aLoadoutWasSelected && aPlanetWasSelected)
        {
            readyButton.PlayAnimationReady();
        }
        else
        {
            readyButton.PlayAnimationDisabled();
        }
    }

    public void StartLevel()
    {
        readyButton.PlayAnimationDisabled();
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

        PlayerSelectionData.selectedWeapon = weaponName;
        PlayerSelectionData.selectedAbility = abilityName;

        AkUnitySoundEngine.PostEvent("TransitionTo_Level", gameObject);

        SceneLoader.Instance.ChangeScene(sceneName);
    }
}
