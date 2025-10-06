using UnityEngine;

public class TestArt : MonoBehaviour
{
    [SerializeField] StationWithEggEffects stationWithEggEffects;

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.C))
        {
            stationWithEggEffects.Close();
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            stationWithEggEffects.Die();
        }
        
    }
}
