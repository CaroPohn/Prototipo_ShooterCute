using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public class CameraChanger : MonoBehaviour
{
    [SerializeField] Transform[] cameras;

    int currentCamIndex;
    int tempCamIndex;

    private void Start()
    {
        currentCamIndex = 0;
    }

    public void TransitionToCamera(int camIndex,float progress)
    {
        if(currentCamIndex != camIndex) 
        {
            tempCamIndex = currentCamIndex;
            currentCamIndex = camIndex;
        }
        Camera.main.transform.position = Vector3.Lerp(cameras[tempCamIndex].position, cameras[currentCamIndex].position, progress);
        Camera.main.transform.rotation = Quaternion.Lerp(cameras[tempCamIndex].rotation, cameras[currentCamIndex].rotation, progress);
    }

    
}
