using System;
using UnityEngine;

public class EggInteraction : MonoBehaviour
{
    [SerializeField] private InputReader inputReader;
    [SerializeField] private Transform playerTransform;

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
            Debug.Log("Good");
        }
        else
        {
            isPlayerCloseEnough = false;
        }
    }

    private void AttemtInteraction()
    {
        if (isPlayerCloseEnough) 
        { 
            OnInteractWithEgg?.Invoke();
        }
    }
}
