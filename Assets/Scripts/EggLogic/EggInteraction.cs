using System;
using UnityEngine;
using UnityEngine.VFX;

public class EggInteraction : MonoBehaviour
{
    [SerializeField] private InputReader inputReader;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform eggHoldingSpot;

    [SerializeField] private GameObject interactText;

    [SerializeField] private StationAnimationHandler stationAnimationHandler;

    [SerializeField] private Animator uIInteractAnimator;

    [SerializeField] private GameObject exclamationUI;

    public event Action OnInteractWithEgg;

    public static event Action OnGrabbingEgg;

    public float minimumDistanceToInteract;

    private float distanceToPlayer;
    private bool isPlayerCloseEnough;

    private bool hasPlayerInteracted;
    private bool canPlayerGrabEgg;

    private bool hasDieStationAnimationFinished;

    public static event Action OnStartWaves;

    private void Start()
    {
        isPlayerCloseEnough = false;
        hasPlayerInteracted = false;
        canPlayerGrabEgg = false;
        hasDieStationAnimationFinished = false;

        AkUnitySoundEngine.PostEvent("Egg_Levitate", gameObject);
    }

    private void OnEnable()
    {
        inputReader.OnInteraction += AttemtInteraction;
        inputReader.OnInteraction += AttemptGivingEgg;
        WaveManager.OnWinningAllWaves += LetPlayerGrabEgg;
        stationAnimationHandler.OnFinishStationDeathAnimation += StationAnimationFinished;
    }

    private void OnDisable()
    {
        inputReader.OnInteraction -= AttemtInteraction;
        inputReader.OnInteraction -= AttemptGivingEgg;
        WaveManager.OnWinningAllWaves -= LetPlayerGrabEgg;
        stationAnimationHandler.OnFinishStationDeathAnimation -= StationAnimationFinished;
    }

    private void Update()
    {
        CalculateDistanceToPlayer();

        if (hasPlayerInteracted) 
        {
            SpawnInteractText(false);
        }
    }

    private void StationAnimationFinished()
    {
        hasDieStationAnimationFinished = true;
    }

    private void CalculateDistanceToPlayer()
    {
        distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        isPlayerCloseEnough = distanceToPlayer < minimumDistanceToInteract;

        if (!hasPlayerInteracted) 
        {
            if (isPlayerCloseEnough)
            {
                SpawnInteractText(isPlayerCloseEnough);
                MakeTextFollowPlayer();
            }
            else
            {
                SpawnInteractText(isPlayerCloseEnough);
            }
        }
    }

    private void AttemtInteraction()
    {
        if (isPlayerCloseEnough && !hasPlayerInteracted) 
        {
            OnInteractWithEgg?.Invoke();

            exclamationUI.SetActive(false);

            hasPlayerInteracted = true;

            AkUnitySoundEngine.PostEvent("Egg_ShieldActivate", gameObject);

            OnStartWaves?.Invoke();
        }
    }

    private void SpawnInteractText(bool isActive)
    {
        //interactText.SetActive(isActive);
        uIInteractAnimator.SetBool("visible", isActive);
    }

    private void MakeTextFollowPlayer()
    {
        Vector3 lookPosition = new Vector3(playerTransform.position.x, interactText.transform.position.y, playerTransform.position.z);
        interactText.transform.LookAt(lookPosition);
    }

    private void LetPlayerGrabEgg()
    {
        canPlayerGrabEgg = true;

        exclamationUI.SetActive(true);
    }

    private void AttemptGivingEgg()
    {
        if (canPlayerGrabEgg && isPlayerCloseEnough && hasDieStationAnimationFinished)
        {
            GivePlayerEggWhenInteracting();
        }
    }

    private void GivePlayerEggWhenInteracting()
    {
        transform.parent = eggHoldingSpot;
        transform.position = eggHoldingSpot.position;
        transform.rotation = eggHoldingSpot.rotation;

        AkUnitySoundEngine.PostEvent("Egg_PickUp", gameObject);
        
        OnGrabbingEgg?.Invoke();
        exclamationUI.SetActive(false);
    }    
}
