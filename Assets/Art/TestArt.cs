using UnityEngine;

public class TestArt : MonoBehaviour
{
    [SerializeField] float initialGlow;
    [SerializeField] EggGlowManager eggGlow;
    bool turnOn = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            turnOn = !turnOn;
            if(turnOn) eggGlow.TurnOnEggGlow();
            else eggGlow.TurnOffEggGlow();

        }
    }
}
