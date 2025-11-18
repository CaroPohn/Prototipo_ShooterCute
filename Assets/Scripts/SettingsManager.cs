using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] private Camera playerCamera;
    [SerializeField] private Slider sensSlider;
    [SerializeField] private Slider fovSlider;

    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    [SerializeField] private GameObject fovTextHolder;
    [SerializeField] private GameObject sensTextHolder;

    [SerializeField] private GameObject masterTextHolder;
    [SerializeField] private GameObject musicTextHolder;
    [SerializeField] private GameObject sfxTextHolder;

    private TextMeshProUGUI fovText;
    private TextMeshProUGUI sensText;

    private TextMeshProUGUI masterText;
    private TextMeshProUGUI musicText;
    private TextMeshProUGUI sfxText;

    [SerializeField] private float baseFOV;
    private float baseVolume = 50;

    public event Action OnSensChange;

    private float sensValue;

    private void Awake()
    {
        fovSlider.onValueChanged.AddListener(FovSliderHoldSound);
        sensSlider.onValueChanged.AddListener(SensSliderHoldSound);

        masterSlider.onValueChanged.AddListener(MasterSliderHoldSound);
        musicSlider.onValueChanged.AddListener(MusicSliderHoldSound);
        sfxSlider.onValueChanged.AddListener(SfxSliderHoldSound);
    }

    private void OnDestroy()
    {
        fovSlider.onValueChanged?.RemoveListener(FovSliderHoldSound);
        sensSlider.onValueChanged?.RemoveListener(SensSliderHoldSound);

        masterSlider.onValueChanged?.RemoveListener(MasterSliderHoldSound);
        musicSlider.onValueChanged?.RemoveListener(MusicSliderHoldSound);
        sfxSlider.onValueChanged?.RemoveListener(SfxSliderHoldSound);
    }

    private void Start()
    {
        float savedSens = PlayerPrefs.GetFloat("Sensitivity", 20.0f);
        float savedFov = PlayerPrefs.GetFloat("FOV", baseFOV);

        float savedMaster = PlayerPrefs.GetFloat("Master", baseVolume);
        float savedMusic = PlayerPrefs.GetFloat("Music", baseVolume);
        float savedSfx = PlayerPrefs.GetFloat("SFX", baseVolume);

        fovText = fovTextHolder.GetComponent<TextMeshProUGUI>();
        sensText = sensTextHolder.GetComponent<TextMeshProUGUI>();

        masterText = masterTextHolder.GetComponent<TextMeshProUGUI>();
        musicText = musicTextHolder.GetComponent<TextMeshProUGUI>();
        sfxText = sfxTextHolder.GetComponent<TextMeshProUGUI>();

        if (playerCamera != null) 
        {
            playerCamera.fieldOfView = savedFov;
        }
       
        sensValue = savedSens;

        fovText.SetText(savedFov.ToString("F0"));
        sensText.SetText(sensValue.ToString("F0"));

        masterText.SetText(savedMaster.ToString("F0"));
        musicText.SetText(savedMusic.ToString("F0"));
        sfxText.SetText(savedSfx.ToString("F0"));

        fovSlider.value = savedFov;
        sensSlider.value = savedSens;

        masterSlider.value = savedMaster;
        musicSlider.value = savedMusic;
        sfxSlider.value = savedSfx;

        fovSlider.onValueChanged.AddListener(ChangeFOV);
        sensSlider.onValueChanged.AddListener(ChangeSens);

        masterSlider.onValueChanged.AddListener(ChangeMaster);
        musicSlider.onValueChanged.AddListener(ChangeMusic);
        sfxSlider.onValueChanged.AddListener(ChangeSFX);

        masterSlider.onValueChanged.AddListener(MasterSliderHoldSound);
        musicSlider.onValueChanged.AddListener(MusicSliderHoldSound);
        sfxSlider.onValueChanged.AddListener(SfxSliderHoldSound);

        Invoke(nameof(InitValues), 1f);
    }

    void InitValues()
    {
        MasterSliderHoldSound(baseVolume);
        MusicSliderHoldSound(baseVolume);
        SfxSliderHoldSound(baseVolume);
    }

    private void ChangeFOV(float newFov)
    {
        if (playerCamera != null)
        {
            playerCamera.fieldOfView = newFov;
        }

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

    private void ChangeMaster(float newMaster)
    {
        PlayerPrefs.SetFloat("Master", newMaster);
        masterText.SetText(newMaster.ToString("F0"));
    }

    private void ChangeMusic(float newMusic)
    {
        PlayerPrefs.SetFloat("Music", newMusic);
        musicText.SetText(newMusic.ToString("F0"));
    }

    private void ChangeSFX(float newSfx)
    {
        PlayerPrefs.SetFloat("SFX", newSfx);
        sfxText.SetText(newSfx.ToString("F0"));
    }

    public float GetSensitivity()
    {
        return sensValue;
    }

    private void FovSliderHoldSound(float value)
    {
        AkUnitySoundEngine.SetRTPCValue("Pitch_Slider", value);
        AkUnitySoundEngine.PostEvent("UI_Slider", gameObject);
    }

    private void SensSliderHoldSound(float value)
    {
        AkUnitySoundEngine.SetRTPCValue("Pitch_Slider", value);
        AkUnitySoundEngine.PostEvent("UI_Slider", gameObject);
    }

    private void MasterSliderHoldSound(float value)
    {
        AkUnitySoundEngine.SetRTPCValue("Pitch_Slider", value);
        AkUnitySoundEngine.PostEvent("UI_Slider", gameObject);

        AkUnitySoundEngine.SetRTPCValue("Master", value);
    }

    private void MusicSliderHoldSound(float value)
    {
        AkUnitySoundEngine.SetRTPCValue("Pitch_Slider", value);
        AkUnitySoundEngine.PostEvent("UI_Slider", gameObject);

        AkUnitySoundEngine.SetRTPCValue("Music", value);
    }

    private void SfxSliderHoldSound(float value)
    {
        AkUnitySoundEngine.SetRTPCValue("Pitch_Slider", value);
        AkUnitySoundEngine.PostEvent("UI_Slider", gameObject);

        AkUnitySoundEngine.SetRTPCValue("SFX", value);
    }
}
