using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;

public class WaveManager : MonoBehaviour
{
    public List<Transform> spawnPoints;
    public List<Wave> waves;

    [SerializeField] private StationWithEggEffects stationEffectsScript;
    [SerializeField] private Collider stationCollider;

    [SerializeField] private ObjectiveUI objectiveUI;

    private int currentWaveIndex = 0;
    private int enemiesAlive = 0;
    private bool spawningWave = false;
    private bool hasRescueTextShow = false;

    private bool hasEggInteractedToStartWaves;

    public static event Action OnWinningAllWaves;

    private void Start()
    {
        hasEggInteractedToStartWaves = false;

        objectiveUI.ShowNewMission("FIND THE EGG", "Locate the endangered Lumming egg");
    }

    private void OnEnable()
    {
        EggInteraction.OnInteractWithEgg += InteractedWithEgg;
        EggInteraction.OnGrabbingEgg += ShowBackToShipText;
    }

    private void OnDisable()
    {
        EggInteraction.OnInteractWithEgg -= InteractedWithEgg;
        EggInteraction.OnGrabbingEgg -= ShowBackToShipText;
    }

    void Update()
    {
        if (!spawningWave && enemiesAlive == 0 && currentWaveIndex < waves.Count && hasEggInteractedToStartWaves)
        {        
            StartCoroutine(StartNextWave());
        }

        CheckIfPlayerHasWinAllWaves();

        RescueTextHandler();
    }

    private void InteractedWithEgg()
    {
        hasEggInteractedToStartWaves = true;

        objectiveUI.HideMissionNotification();

        Invoke(nameof(ShowSurviveMissionText), 2f);

        stationEffectsScript.Close();
    }

    private void CheckIfPlayerHasWinAllWaves()
    {
        int count = 0;

        if (enemiesAlive == 0 && currentWaveIndex == 6 && count == 0) 
        { 
            OnWinningAllWaves?.Invoke();

            hasRescueTextShow = true;

            Debug.Log(hasRescueTextShow);

            stationCollider.enabled = false;
            stationEffectsScript.Die();

            count++;
        }
    }

    private void RescueTextHandler()
    {
        if (hasRescueTextShow)
        {
            objectiveUI.HideMissionNotification();
            Invoke(nameof(ShowRecueText), 2f);

            Invoke(nameof(ChangeRescueBool), 4f);
        }
    }

    private void ChangeRescueBool()
    {
        hasRescueTextShow = false;
    }

    private void ShowSurviveMissionText()
    {
        objectiveUI.ShowNewMission("DEFEND YOURSELF!", "Defeat all enemies");
    }

    private void ShowRecueText()
    {


        objectiveUI.ShowNewMission("RESCUE THE EGG", "Handle with care");
    }

    private void ShowBackToShipText()
    {
        objectiveUI.HideMissionNotification();

        objectiveUI.ShowNewMission("ESCAPE!", "Get back to the ship");
    }
    IEnumerator StartNextWave()
    {
        spawningWave = true;

        yield return new WaitForSeconds(2f);

        Wave wave = waves[currentWaveIndex];

        List<Coroutine> spawnCoroutines = new List<Coroutine>();
        foreach (var instruction in wave.spawnInstructions)
        {
            if (instruction.spawnPointIndex >= 0 && instruction.spawnPointIndex < spawnPoints.Count)
            {
                Coroutine coroutine = StartCoroutine(SpawnEnemies(instruction));
                spawnCoroutines.Add(coroutine);
            }
            else
            {
                Debug.LogWarning("Índice de spawn point inválido en instrucción.");
            }
        }

        currentWaveIndex++;
        spawningWave = false;
    }

    IEnumerator SpawnEnemies(SpawnInstruction instruction)
    {
        Transform spawnPoint = spawnPoints[instruction.spawnPointIndex];

        for (int i = 0; i < instruction.enemyCount; i++)
        {
            GameObject enemy = Instantiate(instruction.enemyPrefab, spawnPoint.position, Quaternion.identity);
            enemiesAlive++;

            HealthSystem enemyHealth = enemy.GetComponent<HealthSystem>();
            if (enemyHealth != null)
            {
                enemyHealth.onDeath += OnEnemyDeath;
            }

            yield return new WaitForSeconds(instruction.spawnInterval);
        }
    }

    void OnEnemyDeath()
    {
        enemiesAlive--;
    }
}

