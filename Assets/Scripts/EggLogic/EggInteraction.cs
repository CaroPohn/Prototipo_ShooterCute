using System;
using TMPro;
using UnityEngine;

public class EggInteraction : MonoBehaviour
{
    [SerializeField] private InputReader inputReader;
    [SerializeField] private Transform playerTransform;

    [SerializeField] private GameObject interactText;

    public static event Action OnInteractWithEgg;

    public float minimumDistanceToInteract;

    private float distanceToPlayer;
    private bool isPlayerCloseEnough;

    private void Start()
    {
        isPlayerCloseEnough = false;
    }

    private void OnEnable()
    {
        inputReader.OnInteraction += AttemtInteraction;
    }

    private void OnDisable()
    {
        inputReader.OnInteraction -= AttemtInteraction;
    }

    private void Update()
    {
        CalculateDistanceToPlayer();
    }

    private void CalculateDistanceToPlayer()
    {
        distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

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

    private void AttemtInteraction()
    {
        if (isPlayerCloseEnough) 
        { 
            OnInteractWithEgg?.Invoke();
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
}
