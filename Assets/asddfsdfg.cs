using UnityEngine;

public class asddfsdfg : MonoBehaviour
{
    public CritterProperties.CritterParameters c;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
     if(Input.GetKeyDown(KeyCode.Escape))
        {
            c.ability.Use();
        }   
    }
}
