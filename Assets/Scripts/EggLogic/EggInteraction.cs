using System;
using UnityEngine;

public class EggInteraction : MonoBehaviour
{
    [SerializeField] private InputReader inputReader;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform eggHoldingSpot;

    [SerializeField] private GameObject interactText;

    [SerializeField] private StationAnimationHandler stationAnimationHandler;

    public static event Action OnInteractWithEgg;

    public static event Action OnGrabbingEgg;

    public float minimumDistanceToInteract;

    private float distanceToPlayer;
    private bool isPlayerCloseEnough;

    private bool hasPlayerInteracted;
    private bool canPlayerGrabEgg;

    private bool hasDieStationAnimationFinished;

    private void Start()
    {
        isPlayerCloseEnough = false;
        hasPlayerInteracted = false;
        canPlayerGrabEgg = false;
        hasDieStationAnimationFinished = false;
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

            hasPlayerInteracted = true;
        }
    }

    private void SpawnInteractText(bool isActive)
    {
        interactText.SetActive(isActive);
    }

    private void MakeTextFollowPlayer()
    {
        interactText.transform.LookAt(playerTransform.position);
    }

    private void LetPlayerGrabEgg()
    {
        canPlayerGrabEgg = true;
        OnGrabbingEgg?.Invoke();
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
    }    
}
