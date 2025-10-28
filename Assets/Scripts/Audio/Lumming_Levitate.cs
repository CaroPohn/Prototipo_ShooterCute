using AK.Wwise;
using UnityEngine;

public class Lumming_Levitate : MonoBehaviour

{
    public GameObject triggerObject = null;
    private bool playerInside = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == triggerObject)
        {        
            playerInside = true;
            AkUnitySoundEngine.PostEvent("Lumming_Levitate", gameObject);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == triggerObject)
        {        
            playerInside = false;
            AkUnitySoundEngine.PostEvent("Lumming_Levitate_Stop", gameObject);
        }
    }

    void OnDisable()
    {
        playerInside = false;
        AkUnitySoundEngine.PostEvent("Lumming_Levitate_Stop", gameObject);
    }

    void OnEnable()
    {
        if (playerInside == true)
        AkUnitySoundEngine.PostEvent("Lumming_Levitate", gameObject);
    }

}
