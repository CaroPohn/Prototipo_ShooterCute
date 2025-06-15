using System;
using UnityEngine;

public class EggInteraction : MonoBehaviour
{
    [SerializeField] private InputReader inputReader;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform eggHoldingSpot;

    [SerializeField] private GameObject interactText;

    public static event Action OnInteractWithEgg;

    public float minimumDistanceToInteract;

    private float distanceToPlayer;
    private bool isPlayerCloseEnough;

    private bool hasPlayerInteracted;
    private bool canPlayerGrabEgg;

    private void Start()
    {
        isPlayerCloseEnough = false;
        hasPlayerInteracted = false;
        canPlayerGrabEgg = false;
    }

    private void OnEnable()
    {
        inputReader.OnInteraction += AttemtInteraction;
        inputReader.OnInteraction += AttemptGivingEgg;
        WaveManager.OnWinningAllWaves += LetPlayerGrabEgg;
    }

    private void OnDisable()
    {
        inputReader.OnInteraction -= AttemtInteraction;
        inputReader.OnInteraction -= AttemptGivingEgg;
        WaveManager.OnWinningAllWaves -= LetPlayerGrabEgg;
    }

    private void Update()
    {
        CalculateDistanceToPlayer();

        if (hasPlayerInteracted) 
        {
            SpawnInteractText(false);
        }
    }

    private void CalculateDistanceToPlayer()
    {
        distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        if (!hasPlayerInteracted ) 
        {
            if (distanceToPlayer < minimumDistanceToInteract)
            {
                isPlayerCloseEnough = true;
                SpawnInteractText(isPlayerCloseEnough);
                MakeTextFollowPlayer();
            }
            else
            {
                isPlayerCloseEnough = false;
                SpawnInteractText(isPlayerCloseEnough);
            }
        }
    }

    private void AttemtInteraction()
    {
        if (isPlayerCloseEnough) 
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
    }

    private void AttemptGivingEgg()
    {
        if (canPlayerGrabEgg && isPlayerCloseEnough)
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
