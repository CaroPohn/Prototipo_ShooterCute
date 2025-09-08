using UnityEngine;

public class TestArt : MonoBehaviour
{
    [SerializeField] float initialGlow;
    [SerializeField] EggGlowManager eggGlow;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            initialGlow += 0.2f;
            eggGlow.GlowIntensity = initialGlow;

        }
    }
}
