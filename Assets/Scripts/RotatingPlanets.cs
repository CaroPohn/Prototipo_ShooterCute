using UnityEngine;

public class RotatingPlanets : MonoBehaviour
{
    [SerializeField] World[] worldsInOrder;
    World CurrentWorld = World.Lava;
    int index = 0;
    [SerializeField] Transform planetsRotationCenter;
    [SerializeField] Transform[] planetsOffsets;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public World RotateToLeft()
    {
        index--;
        if (index < 0)
        {
            index = worldsInOrder.Length - 1;
        }
        CurrentWorld = worldsInOrder[index];
        RotateLeft();
        Debug.Log(CurrentWorld.ToString());
        return CurrentWorld;
    }
    public World RotateToRight()
    {
        index++;
        if (index >= worldsInOrder.Length)
        {
            index = 0;
        }
        CurrentWorld = worldsInOrder[index];
        RotateRight();
        Debug.Log(CurrentWorld.ToString());
        return CurrentWorld;
        
    }
    void RotateLeft()
    {
        planetsRotationCenter.Rotate(new Vector3(0, -120, 0));
        foreach(Transform planetOffset in planetsOffsets) 
        {
            planetOffset.Rotate(new Vector3(0, 120, 0));
        }
    }
    void RotateRight()
    {
        planetsRotationCenter.Rotate(new Vector3(0, 120, 0));
        foreach (Transform planetOffset in planetsOffsets)
        {
            planetOffset.Rotate(new Vector3(0, -120, 0));
        }
    }
}
