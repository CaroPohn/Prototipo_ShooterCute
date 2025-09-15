using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SelectionMenu : MonoBehaviour
{
    [System.Serializable]
    public struct SelectableLumming
    {
        public string weaponName;
        public string abilityName;
        public Sprite icon;
        public GameObject lummingModel;
    }

    public SelectableLumming[] elements;

    public Image weaponDisplay;
    public Image abilityDisplay;

    private string weaponName;
    private string abilityName;

    private int currentWeaponIndex = 0;
    private int currentAbilityIndex = 1;

    public Button nextWeapon;
    public Button prevWeapon;

    public Button nextAbility;
    public Button prevAbility;

    public Button confirmButton;

    private void Awake()
    {
        confirmButton.onClick.AddListener(StartPlayScene);

        nextWeapon.onClick.AddListener(NextWeapon);
        prevWeapon.onClick.AddListener(PrevWeapon);

        nextAbility.onClick.AddListener(NextAbility);
        prevAbility.onClick.AddListener(PrevAbility);
    }

    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void OnDestroy()
    {
        confirmButton.onClick.RemoveListener(StartPlayScene);

        nextWeapon.onClick.RemoveListener(NextWeapon);
        prevWeapon.onClick.RemoveListener(PrevWeapon);

        nextAbility.onClick.RemoveListener(NextAbility);
        prevAbility.onClick.RemoveListener(PrevAbility);
    }

    void Start()
    {
        UpdateDisplays();
    }

    public void NextWeapon()
    {
        currentWeaponIndex = (currentWeaponIndex + 1) % elements.Length;

        if (elements.Length == 2)
        {
            currentAbilityIndex = 1 - currentWeaponIndex;
        }
        else if (currentWeaponIndex == currentAbilityIndex)
        {
            currentWeaponIndex = (currentWeaponIndex + 1) % elements.Length;
        }

        UpdateDisplays();
    }

    public void PrevWeapon()
    {
        currentWeaponIndex = (currentWeaponIndex - 1 + elements.Length) % elements.Length;

        if (elements.Length == 2)
        {
            currentAbilityIndex = 1 - currentWeaponIndex;
        }
        else if (currentWeaponIndex == currentAbilityIndex)
        {
            currentWeaponIndex = (currentWeaponIndex - 1 + elements.Length) % elements.Length;
        }

        UpdateDisplays();
    }

    public void NextAbility()
    {
        currentAbilityIndex = (currentAbilityIndex + 1) % elements.Length;

        if (elements.Length == 2)
        {
            currentWeaponIndex = 1 - currentAbilityIndex;
        }
        else if (currentAbilityIndex == currentWeaponIndex)
        {
            currentAbilityIndex = (currentAbilityIndex + 1) % elements.Length;
        }

        UpdateDisplays();
    }

    public void PrevAbility()
    {
        currentAbilityIndex = (currentAbilityIndex - 1 + elements.Length) % elements.Length;

        if (elements.Length == 2)
        {
            currentWeaponIndex = 1 - currentAbilityIndex;
        }
        else if (currentAbilityIndex == currentWeaponIndex)
        {
            currentAbilityIndex = (currentAbilityIndex - 1 + elements.Length) % elements.Length;
        }

        UpdateDisplays();
    }

    private void UpdateDisplays()
    {
        weaponDisplay.sprite = elements[currentWeaponIndex].icon;
        abilityDisplay.sprite = elements[currentAbilityIndex].icon;

        weaponName = elements[currentWeaponIndex].weaponName;
        abilityName = elements[currentAbilityIndex].abilityName;

        for (int i = 0; i < elements.Length; i++)
        {
            if (elements[i].lummingModel != null)
            {
                elements[i].lummingModel.SetActive(i == currentWeaponIndex);
            }
        }
    }

    private void StartPlayScene()
    {
        PlayerSelectionData.selectedWeapon = weaponName;
        PlayerSelectionData.selectedAbility = abilityName;

        SceneLoader.Instance.ChangeScene("ShooterProto");
    }
}
