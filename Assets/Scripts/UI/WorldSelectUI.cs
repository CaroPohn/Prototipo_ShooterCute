using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.UI;

public class WorldSelectUI : MonoBehaviour
{
    World currentWorld = World.Lava;
    [SerializeField] Ready_Button_Spaceship chooseButton;
    [SerializeField] SummaryUI summaryUI;
    [SerializeField] RotatingPlanets rotatingPlanets;
    [SerializeField] CurrentPlanetInformation currentPlanetInformation;

    private string wiseEventName = "UI_Button_Normal";

    bool planetSelected = false;


    public World GetCurrentWorld()
    {
        return currentWorld;
    }
    public void MoveRight()
    {
        currentWorld = rotatingPlanets.RotateToRight();
        currentPlanetInformation.UpdateDataToPlanet(currentWorld);
        CheckIfChooseButtonShouldBeActive();
    }
    public void MoveLeft()
    {
        currentWorld = rotatingPlanets.RotateToLeft();
        currentPlanetInformation.UpdateDataToPlanet(currentWorld);
        CheckIfChooseButtonShouldBeActive();
    }
    void CheckIfChooseButtonShouldBeActive()
    {
        if ((currentWorld != World.Lava) || ((currentWorld == World.Lava) &&(planetSelected)))
        {
            chooseButton.PlayAnimationDisabled();
        }
        else chooseButton.PlayAnimationReady();
    }
    public void Choose()
    {
        chooseButton.PlayAnimationDisabled();
        if (!planetSelected)
        {
            planetSelected = true;
            summaryUI.UpdatePlanet(currentWorld);

            AkUnitySoundEngine.PostEvent(wiseEventName, gameObject);
        }
        else
        {
            planetSelected = false;
        }
        FindFirstObjectByType<SpaceshipZoneSelectorUI>().MoveRight();
    }
}
