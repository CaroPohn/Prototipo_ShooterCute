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
    }

}
