using UnityEngine;

[System.Serializable]
public class SpawnInstruction
{
    public GameObject enemyPrefab;
    public int enemyCount;
    public float spawnInterval;
    public int spawnPointIndex;
}
