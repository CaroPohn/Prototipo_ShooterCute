using System;
using UnityEngine;

public class EggInteraction : MonoBehaviour
{
    [SerializeField] private InputReader inputReader;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform eggHoldingSpot;

    private FadeEggManager fadeEggManagerScript;

    [SerializeField] private GameObject interactText;

    public static event Action OnInteractWithEgg;

    public float minimumDistanceToInteract;

    private float distanceToPlayer;
    private bool isPlayerCloseEnough;

    private bool hasPlayerInteracted;
    private bool canPlayerGrabEgg;

    private float corruptionFloat;

    private void Start()
    {
        isPlayerCloseEnough = false;
        hasPlayerInteracted = false;
        canPlayerGrabEgg = false;

        fadeEggManagerScript = GetComponent<FadeEggManager>();
    }

    private void OnEnable()
    {
        inputReader.OnInteraction += AttemtInteraction;
        inputReader.OnInteraction += AttemptGivingEgg;
        WaveManager.OnWinningAllWaves += LetPlayerGrabEgg;
        WaveManager.OnNewWave += CorruptionEggEffectManager;
    }

    private void OnDisable()
    {
        inputReader.OnInteraction -= AttemtInteraction;
        inputReader.OnInteraction -= AttemptGivingEgg;
        WaveManager.OnWinningAllWaves -= LetPlayerGrabEgg;
        WaveManager.OnNewWave -= CorruptionEggEffectManager;
    }

    private void Update()
    {
        CalculateDistanceToPlayer();

        if (hasPlayerInteracted) 
        {
            SpawnInteractText(false);
        }
    }

    private void CorruptionEggEffectManager()
    {
        corruptionFloat += 0.18f;

        fadeEggManagerScript.UpdateEggFadeProgress(corruptionFloat);
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
