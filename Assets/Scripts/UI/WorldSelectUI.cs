using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.UI;

public class WorldSelectUI : SpaceShipZoneScreen
{
    World currentWorld = World.Lava;
    [SerializeField] Ready_Button_Spaceship chooseButton;
    [SerializeField] RotatingPlanets rotatingPlanets;
    [SerializeField] CurrentPlanetInformation currentPlanetInformation;
    [SerializeField] GameObject container;


    public override void HideAllScreens()
    {
        container.SetActive(false);
    }
    public override void ShowAllScreens()
    {
        container.SetActive(true);
        CheckIfChooseButtonShouldBeActive();
    }

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
        FindFirstObjectByType<SpaceshipZoneSelectorUI>().MoveRight();
    }
}
