using UnityEngine;
using UnityEngine.UI;

public class SummaryUI : MonoBehaviour
{
    [SerializeField] Image weaponImage;
    [SerializeField] Image abilityImage;
    [SerializeField] Sprite[] lummingsSprites;
    [SerializeField] Image planetImage;
    [SerializeField] Sprite[] planetsSprites;
    [SerializeField] CameraChanger changer;
    [SerializeField] Image worldImage;
    [SerializeField] Sprite lavaWorldSprite;
    [SerializeField] GameObject infoDescriptionGO;
    [SerializeField] Ready_Button_Spaceship readyButton;
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
        aPlanetWasSelected = (world != World.None);
        if(world == World.Lava)
        {
            infoDescriptionGO.SetActive(true);
            worldImage.sprite = lavaWorldSprite;
        }
        TurnOnOffLever();

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
    }

}
