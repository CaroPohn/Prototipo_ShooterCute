using UnityEngine;


public class HealthPackHolder : MonoBehaviour
{
    private float timer;
    public float cooldown;

    private bool hasHealthPackBeenGrabbed;

    [SerializeField] private GameObject healthPack;
    [SerializeField] private GameObject cooldownEffectGO;

    [SerializeField] private HealthPackSystem healthPackSystem;

    [SerializeField] private Transform modelTransform;

    [SerializeField] private GameObject player;

    [SerializeField] private Sprite activeHealthPackSprite;
    [SerializeField] private Sprite inactiveHealthPackSprite;

    private Vector3 normalScale;

    private SpriteRenderer spriteRenderer;

    private void OnEnable()
    {
        healthPackSystem.OnGrabingHealthPack += GrabHealthPack;
        healthPackSystem.OnGrabingHealthPack += ChangeGrabbedBool;

        spriteRenderer = gameObject.GetComponentInChildren<SpriteRenderer>();
    }

    private void OnDisable()
    {
        healthPackSystem.OnGrabingHealthPack -= GrabHealthPack;
        healthPackSystem.OnGrabingHealthPack -= ChangeGrabbedBool;
    }

    private void Start()
    {
        normalScale = spriteRenderer.gameObject.transform.localScale;

        spriteRenderer.sprite = activeHealthPackSprite;

        timer = 0;
        hasHealthPackBeenGrabbed = false;
        cooldownEffectGO.SetActive(false);
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (hasHealthPackBeenGrabbed) 
        { 
            ReactivateHealthPack();
        }

        modelTransform.LookAt(player.transform);
    }

    private void GrabHealthPack()
    {
        healthPack.SetActive(false);
        cooldownEffectGO.SetActive(true);

        spriteRenderer.sprite = inactiveHealthPackSprite;

        spriteRenderer.gameObject.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

        timer = 0;
    }

    private void ReactivateHealthPack()
    {
        if (timer > cooldown) 
        {
            ChangeGrabbedBool();
            healthPack.SetActive(true);
            cooldownEffectGO.SetActive(false);

            spriteRenderer.sprite = activeHealthPackSprite;
            spriteRenderer.gameObject.transform.localScale = normalScale;
        }
    }

    private void ChangeGrabbedBool()
    {
        hasHealthPackBeenGrabbed = !hasHealthPackBeenGrabbed;
    }
}
