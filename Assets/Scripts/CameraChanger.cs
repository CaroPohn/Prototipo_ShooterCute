using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.Rendering;

public class CameraChanger : MonoBehaviour
{
    [SerializeField] Transform[] cameras;
    [SerializeField] AnimationCurve curve;
    [SerializeField] Volume loadoutVolume;
    [SerializeField] AnimationCurve loadoutCurve;

    int loadoutIndex = 1;
    int currentCamIndex;
    int tempCamIndex;

    private void Start()
    {
        currentCamIndex = 0;
        tempCamIndex = 0;
    }

    public void TransitionToCamera(int camIndex,float progress)
    {
        if(currentCamIndex != camIndex) 
        {
            tempCamIndex = currentCamIndex;
            currentCamIndex = camIndex;
        }
        float curveSmooth = curve.Evaluate(progress);
        Camera.main.transform.position = Vector3.Lerp(cameras[tempCamIndex].position, cameras[currentCamIndex].position, curveSmooth);
        Camera.main.transform.rotation = Quaternion.Lerp(cameras[tempCamIndex].rotation, cameras[currentCamIndex].rotation, curveSmooth);

        float loadoutVolumeSmooth = loadoutCurve.Evaluate(progress);
        if (currentCamIndex == 1) loadoutVolume.weight = loadoutVolumeSmooth;
        else loadoutVolume.weight = 1f - loadoutVolumeSmooth;
    }

    
}
