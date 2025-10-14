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

    [SerializeField] private TextMeshProUGUI waveText;

    private int currentWaveIndex = 0;
    private int enemiesAlive = 0;
    private bool spawningWave = false;

    private bool hasEggInteractedToStartWaves;

    public static event Action OnWinningAllWaves;
    //public static event Action OnNewWave;

    private void Start()
    {
        hasEggInteractedToStartWaves = false;
    }

    private void OnEnable()
    {
        EggInteraction.OnInteractWithEgg += InteractedWithEgg;
    }

    private void OnDisable()
    {
        EggInteraction.OnInteractWithEgg -= InteractedWithEgg;
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

        stationEffectsScript.Close();
    }

    private void CheckIfPlayerHasWinAllWaves()
    {
        if (enemiesAlive == 0 && currentWaveIndex == 6) 
        { 
            OnWinningAllWaves?.Invoke();

            stationCollider.enabled = false;
            stationEffectsScript.Die();
        }
    }

    IEnumerator StartNextWave()
    {
        //OnNewWave?.Invoke();

        spawningWave = true;

        waveText.gameObject.SetActive(true);

        yield return new WaitForSeconds(2f); 

        waveText.gameObject.SetActive(false);

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

