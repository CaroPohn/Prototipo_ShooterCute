using System;
using UnityEngine;
using UnityEngine.UI;

public class SelectionMenu : MonoBehaviour
{
    public Sprite[] elementSprites;

    public Image weaponDisplay;
    public Image abilityDisplay;

    public Button nextWeapon;
    public Button prevWeapon;

    public Button nextAbility;
    public Button prevAbility;

    public Button confirmButton;

    private int currentWeaponIndex = 0;
    private int currentAbilityIndex = 1;

    private void Awake()
    {
        confirmButton.onClick.AddListener(StartPlayScene);

        nextWeapon.onClick.AddListener(NextWeapon);
        prevWeapon.onClick.AddListener(PrevWeapon);

        nextAbility.onClick.AddListener(NextAbility);
        prevAbility.onClick.AddListener(PrevAbility);
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
        currentWeaponIndex = (currentWeaponIndex + 1) % elementSprites.Length;

        if (elementSprites.Length == 2)
        {
            currentAbilityIndex = 1 - currentWeaponIndex;
        }
        else if (currentWeaponIndex == currentAbilityIndex)
        {
            currentWeaponIndex = (currentWeaponIndex + 1) % elementSprites.Length;
        }

        UpdateDisplays();
    }

    public void PrevWeapon()
    {
        currentWeaponIndex = (currentWeaponIndex - 1 + elementSprites.Length) % elementSprites.Length;

        if (elementSprites.Length == 2)
        {
            currentAbilityIndex = 1 - currentWeaponIndex;
        }
        else if (currentWeaponIndex == currentAbilityIndex)
        {
            currentWeaponIndex = (currentWeaponIndex - 1 + elementSprites.Length) % elementSprites.Length;
        }

        UpdateDisplays();
    }

    public void NextAbility()
    {
        currentAbilityIndex = (currentAbilityIndex + 1) % elementSprites.Length;

        if (elementSprites.Length == 2)
        {
            currentWeaponIndex = 1 - currentAbilityIndex;
        }
        else if (currentAbilityIndex == currentWeaponIndex)
        {
            currentAbilityIndex = (currentAbilityIndex + 1) % elementSprites.Length;
        }

        UpdateDisplays();
    }

    public void PrevAbility()
    {
        currentAbilityIndex = (currentAbilityIndex - 1 + elementSprites.Length) % elementSprites.Length;

        if (elementSprites.Length == 2)
        {
            currentWeaponIndex = 1 - currentAbilityIndex;
        }
        else if (currentAbilityIndex == currentWeaponIndex)
        {
            currentAbilityIndex = (currentAbilityIndex - 1 + elementSprites.Length) % elementSprites.Length;
        }

        UpdateDisplays();
    }

    private void UpdateDisplays()
    {
        weaponDisplay.sprite = elementSprites[currentWeaponIndex];
        abilityDisplay.sprite = elementSprites[currentAbilityIndex];
    }

    private void StartPlayScene()
    {
        SceneLoader.Instance.ChangeScene("ShooterProto");
    }
}
