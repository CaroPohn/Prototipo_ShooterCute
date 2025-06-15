using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;

public class WaveManager : MonoBehaviour
{
    public List<Transform> spawnPoints;
    public List<Wave> waves;

    [SerializeField] private TextMeshProUGUI waveText;

    private int currentWaveIndex = 0;
    private int enemiesAlive = 0;
    private bool spawningWave = false;

    private bool hasEggInteractedToStartWaves;

    public static event Action OnWinningAllWaves;

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
    }

    private void CheckIfPlayerHasWinAllWaves()
    {
        if (enemiesAlive == 0 && currentWaveIndex == 6) 
        { 
            OnWinningAllWaves?.Invoke();
        }
    }

    IEnumerator StartNextWave()
    {
        spawningWave = true;
        
        if(currentWaveIndex == 5)
        {
            waveText.text = "¡¡¡Final Wave Incoming!!!";
        }
        else
        {
            waveText.text = "¡¡¡Wave " + (currentWaveIndex + 1) + " Incoming!!!";
        }

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

