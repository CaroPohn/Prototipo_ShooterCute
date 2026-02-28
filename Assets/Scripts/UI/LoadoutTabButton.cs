using UnityEngine;

public class LoadoutTabButton : MonoBehaviour
{
    LoadoutUI loadoutUI;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Clicked()
    {
        if(loadoutUI == null) 
        {
            loadoutUI = FindFirstObjectByType<LoadoutUI>();
        }
        //loadoutUI.TabPressed(this);
    }
    public void BringToFront()
    {

    }
    public void SendToTheBack()
    {

    }
}
