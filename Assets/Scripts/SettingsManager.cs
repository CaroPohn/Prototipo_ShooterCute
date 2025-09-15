using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] private Camera playerCamera;
    [SerializeField] private Slider sensSlider;
    [SerializeField] private Slider fovSlider;

    [SerializeField] private GameObject fovTextHolder;
    [SerializeField] private GameObject sensTextHolder;

    private TextMeshProUGUI fovText;
    private TextMeshProUGUI sensText;

    [SerializeField] private float baseFOV;

    public event Action OnSensChange;

    private float sensValue;

    private void Start()
    {
        float savedSens = PlayerPrefs.GetFloat("Sensitivity", 20.0f);
        float savedFov = PlayerPrefs.GetFloat("FOV", baseFOV);

        fovText = fovTextHolder.GetComponent<TextMeshProUGUI>();
        sensText = sensTextHolder.GetComponent<TextMeshProUGUI>();

        playerCamera.fieldOfView = savedFov;
        sensValue = savedSens;

        fovText.SetText(savedFov.ToString("F0"));
        sensText.SetText(sensValue.ToString("F0"));

        fovSlider.value = savedFov;
        sensSlider.value = savedSens;

        fovSlider.onValueChanged.AddListener(ChangeFOV);
        sensSlider.onValueChanged.AddListener(ChangeSens);
    }

    private void ChangeFOV(float newFov)
    {
        playerCamera.fieldOfView = newFov;
        PlayerPrefs.SetFloat("FOV", newFov);
        fovText.SetText(newFov.ToString("F0"));
    }

    private void ChangeSens(float newSens)
    {
        sensValue = newSens;
        PlayerPrefs.SetFloat("Sensitivity", newSens);
        OnSensChange?.Invoke();
        sensText.SetText(newSens.ToString("F0"));
    }

    public float GetSensitivity()
    {
        return sensValue;
    }
}
