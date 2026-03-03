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

    [SerializeField] private EggInteraction eggInteracionScript;
    [SerializeField] private WaveProgressBar waveProgressBar;

    private int currentWaveIndex = 0;
    private int enemiesAlive = 0;
    private bool spawningWave = false;
    private bool hasRescueTextShow = false;
    private int totalEnemies = 0;
    private int enemiesKilled = 0;

    private bool hasEggInteractedToStartWaves;

    public static event Action OnWinningAllWaves;

    public static event Action OnWaveEasy;
    public static event Action OnWaveMedium;
    public static event Action OnWaveHard;

    private void Start()
    {
        hasEggInteractedToStartWaves = false;

        objectiveUI.ShowNewMission("FIND THE EGG", "Locate the endangered Lumming egg");
        CalculateHowManyEnemies();
    }

    private void OnEnable()
    {
        eggInteracionScript.OnInteractWithEgg += InteractedWithEgg;
        EggInteraction.OnGrabbingEgg += ShowShipText;
        OnWinningAllWaves += RescueTextHandler;
    }

    private void OnDisable()
    {
        eggInteracionScript.OnInteractWithEgg -= InteractedWithEgg;
        EggInteraction.OnGrabbingEgg -= ShowShipText;
        OnWinningAllWaves -= RescueTextHandler;
    }

    void Update()
    {
        if (!spawningWave && enemiesAlive == 0 && currentWaveIndex < waves.Count && hasEggInteractedToStartWaves)
        {
            StartCoroutine(StartNextWave());
        }

        CheckIfPlayerHasWinAllWaves();
    }

    private void InteractedWithEgg()
    {
        hasEggInteractedToStartWaves = true;

        objectiveUI.HideMissionNotification();

        Invoke(nameof(ShowSurviveMissionText), 2f);

        stationEffectsScript.Close();
        Invoke("DisplayProgressBar", 3.5f) ;
    }

    private void CheckIfPlayerHasWinAllWaves()
    {
        if (enemiesAlive == 0 && currentWaveIndex == 6 && !hasRescueTextShow)
        {
            OnWinningAllWaves?.Invoke();

            hasRescueTextShow = true;

            stationCollider.enabled = false;
            stationEffectsScript.Die();
            waveProgressBar.HideProgressBar();
        }
    }

    private void RescueTextHandler()
    {
        objectiveUI.HideMissionNotification();
        Invoke(nameof(ShowRescueText), 2f);
    }

    private void ShowSurviveMissionText()
    {
        objectiveUI.ShowNewMission("DEFEND YOURSELF!", "Defeat all enemies");
    }

    private void ShowRescueText()
    {
        objectiveUI.ShowNewMission("RESCUE THE EGG", "Handle with care");
    }

    private void ShowShipText()
    {
        objectiveUI.HideMissionNotification();
        Invoke(nameof(ShowBackToShipText), 2f);
    }

    private void ShowBackToShipText()
    {
        objectiveUI.ShowNewMission("ESCAPE!", "Get back to the ship");
    }
    IEnumerator StartNextWave()
    {
        spawningWave = true;

        yield return new WaitForSeconds(2f);

        Wave wave = waves[currentWaveIndex];

        if (currentWaveIndex == 0)
        {
            OnWaveEasy.Invoke();
        }
        else if (currentWaveIndex == 3)
        {
            OnWaveMedium.Invoke();
        }
        else if (currentWaveIndex == 5)
        {
            OnWaveHard.Invoke();
        }

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
        enemiesKilled++;
        UpdateProgressBar();
    }
    private void CalculateHowManyEnemies()
    {
        foreach(Wave wave in waves) 
        {
            foreach(SpawnInstruction spawnInst in wave.spawnInstructions)
            {
                totalEnemies = totalEnemies + spawnInst.enemyCount;
            }
        }
    }
    private void UpdateProgressBar()
    {
        waveProgressBar.UpdateProgress((float)enemiesKilled/(float)totalEnemies);
    }
    private void DisplayProgressBar()
    {
        waveProgressBar.DisplayProgressBar();
    }
}

