using UnityEngine;
using UnityEngine.UI;

public class WeaponChanger : MonoBehaviour 
{
    [SerializeField] private GameObject arms;

    [SerializeField] private GameObject bomb;
    [SerializeField] private GameObject electric;

    [SerializeField] private GameObject bombHolder;
    [SerializeField] private GameObject gunHandler;

    [SerializeField] public Image FillAbilityImage;

    public float timer; 

    public int weaponIndex;

    private string weaponName;
    private string abilityName;

    [SerializeField] private PlayerWeaponChoose playerWeaponChooseScript;

    [SerializeField] private InputReader inputReader;

    GameObject selectedGun;
    GameObject selectedAbility;

    private void Start()
    {
        weaponName = PlayerSelectionData.selectedWeapon;
        abilityName = PlayerSelectionData.selectedAbility;

        selectedGun = FindChildWithTag(gunHandler.transform, weaponName);
        selectedAbility = GetAbilityPrefabByTag(abilityName);

        weaponIndex = 1;
        ChangeWeapon();
    }

    private void OnEnable()
    {
        inputReader.OnUseAbility += ChangeToAbility;
        inputReader.OnChangeToWeapon += ChangeToWeapon;
    }

    private void OnDisable()
    {
        inputReader.OnUseAbility -= ChangeToAbility;
        inputReader.OnChangeToWeapon -= ChangeToWeapon;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (FillAbilityImage.fillAmount <= 1)
        {
            FillAbilityImage.fillAmount += (1f / 10f) * Time.deltaTime;
        }
    }

    private void ChangeToWeapon()
    {
        weaponIndex = 1;
        ChangeWeapon();
    }

    private void ChangeToAbility()
    {
        weaponIndex = 2;
        ChangeWeapon();
    }

    public void ChangeWeapon()
    {
        if (weaponIndex == 1)
        {
            if (selectedGun != null)
                selectedGun.SetActive(true);

            arms.SetActive(true);
        }
        else if (weaponIndex == 2 && timer >= 10.0f)
        {
            selectedGun.SetActive(false);
            arms.SetActive(false);
        
            if (selectedAbility != null)
            {
                selectedAbility.SetActive(true);
            }
        }
    }

    GameObject FindChildWithTag(Transform parent, string tag)
    {
        foreach (Transform child in parent.transform)
        {
            if (child.CompareTag(tag))
            {
                return child.gameObject;
            }
        }

        return null;
    }

    GameObject GetAbilityPrefabByTag(string tag)
    {
        if (tag == "ElectricAbility")
            return electric;
        if (tag == "BombAbility")
            return bomb;

        return null;
    }
}

