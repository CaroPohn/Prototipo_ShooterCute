using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.UI;

public class WorldSelectUI : MonoBehaviour
{
    [SerializeField] World[] worldsInOrder;
    World CurrentWorld = World.Lava;
    int index = 0;
    [SerializeField] Transform planetsRotationCenter;
    [SerializeField] GameObject moveRightButtonGO;
    [SerializeField] GameObject moveLeftButtonGO;
    [SerializeField] GameObject infoPanel;
    [SerializeField] Button chooseButton;
    [SerializeField] SummaryUI summaryUI;

    [SerializeField] TextMeshProUGUI ChoosetmpUGUI;
    bool planetSelected = false;
    void Start()
    {
        
    }

    public World GetCurrentWorld()
    {
        return CurrentWorld;
    }
    public void moveLeft()
    {
        index--;
        if(index < 0)
        {
            index = worldsInOrder.Length - 1;
        }
        CurrentWorld = worldsInOrder[index];
        RotateLeft();
        CheckIfChooseButtonShouldBeActive();
    }
    void RotateLeft()
    {
        planetsRotationCenter.Rotate(new Vector3(0, -360 / 3, 0));
    }
    void RotateRight()
    {
        planetsRotationCenter.Rotate(new Vector3(0, 360 / 3, 0));
    }
    public void moveRight()
    {
        index++;
        if (index >= worldsInOrder.Length)
        {
            index = 0;
        }
        CurrentWorld = worldsInOrder[index];
        RotateRight();
        CheckIfChooseButtonShouldBeActive();
    }
    public void CheckInfo()
    {
        if (!infoPanel.activeSelf)
        {
            moveRightButtonGO.SetActive(false);
            moveLeftButtonGO.SetActive(false);
            infoPanel.SetActive(true);
        }
        else
        {
            moveRightButtonGO.SetActive(true);
            moveLeftButtonGO.SetActive(true);
            infoPanel.SetActive(false);
        }
    }
    void CheckIfChooseButtonShouldBeActive()
    {
        if (CurrentWorld != World.Lava)
        {
            chooseButton.interactable = false;
        }
        else chooseButton.interactable = true;
    }
    public void Choose()
    {
        if (!planetSelected)
        {
            planetSelected = true;
            ChoosetmpUGUI.text = "Cancel selection";
            summaryUI.UpdatePlanet(CurrentWorld);
        }
        else
        {
            planetSelected = false;
            ChoosetmpUGUI.text = "Choose";
        }
        moveRightButtonGO.SetActive(!planetSelected);
        moveLeftButtonGO.SetActive(!planetSelected);
        infoPanel.SetActive(false);

    }
    void Update()
    {
        
    }
}
