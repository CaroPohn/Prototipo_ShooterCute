using UnityEngine;

[CreateAssetMenu(fileName = "WaveParameters", menuName = "Wave Parameters/New Wave")]
public class WaveParameters : ScriptableObject
{
    [System.Serializable]
    public class WaveData
    {
        public GameObject enemyPrefab; // Prefab del enemigo
        public int count;              // Cantidad de enemigos en esta wave
        public float spawnDelay;       // Retraso entre spawns de este enemigo
    }

    public WaveData[] waveEntries;    // Lista de enemigos en esta wave
    
    public bool isTimeNeeded = false;
    public float timeBetweenWaves = 5f; // Tiempo antes de la próxima wave

    public bool isCountNeeded = false;
    public int cantKillsBetweenWaves = 0;

    public bool isInfinite = false;    // ¿Es una wave infinita?
    public float infiniteSpawnRate = 1f; // Tasa de spawn si es infinita
}
