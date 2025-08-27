using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] private Camera playerCamera;
    [SerializeField] private Slider sensSlider;
    [SerializeField] private Slider fovSlider;

    public event Action OnSensChange;

    private float sensValue;

    private void Start()
    {
        float savedSens = PlayerPrefs.GetFloat("Sensitivity", 20.0f);
        float savedFov = PlayerPrefs.GetFloat("FOV", 60.0f);

        playerCamera.fieldOfView = savedFov;
        sensValue = savedSens;

        fovSlider.value = savedFov;
        sensSlider.value = savedSens;

        fovSlider.onValueChanged.AddListener(ChangeFOV);
        sensSlider.onValueChanged.AddListener(ChangeSens);
    }

    private void ChangeFOV(float newFov)
    {
        playerCamera.fieldOfView = newFov;
        PlayerPrefs.SetFloat("FOV", newFov);
    }

    private void ChangeSens(float newSens)
    {
        sensValue = newSens;
        PlayerPrefs.SetFloat("Sensitivity", newSens);
        OnSensChange?.Invoke();
    }

    public float GetSensitivity()
    {
        return sensValue;
    }
}
