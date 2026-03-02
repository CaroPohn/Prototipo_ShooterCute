using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TriggerZone
{
    public string zoneName = "Aisle 1";
    public Collider triggerCollider;
    [HideInInspector] public bool hasTriggered = false;

    public List<PatrolEnemy> patrolEnemiesToSpawn;
    public List<MeleeEnemy> meleeEnemiesToSpawn;

    public List<Transform> spawnPoints;
}

public class AisleEnemyManager : MonoBehaviour
{
    [SerializeField] private List<TriggerZone> aisleZones;

    private void Start()
    {
        foreach (TriggerZone zone in aisleZones)
        {
            if (zone.triggerCollider != null)
            {
                AisleTrigger detector = zone.triggerCollider.gameObject.AddComponent<AisleTrigger>();
                detector.Setup(this, zone);
            }
        }
    }

    public void ActivateZone(TriggerZone zone)
    {
        if (zone.hasTriggered) return;

        zone.hasTriggered = true;

        int spawnIndex = 0;

        foreach (PatrolEnemy patrolPrefab in zone.patrolEnemiesToSpawn)
        {
            Vector3 pos = GetSpawnPosition(zone, spawnIndex);
            Instantiate(patrolPrefab, pos, Quaternion.identity);
            spawnIndex++;
        }

        foreach (MeleeEnemy meleePrefab in zone.meleeEnemiesToSpawn)
        {
            Vector3 pos = GetSpawnPosition(zone, spawnIndex);
            Instantiate(meleePrefab, pos, Quaternion.identity);
            spawnIndex++;
        }
    }

    private Vector3 GetSpawnPosition(TriggerZone zone, int index)
    {
        if (zone.spawnPoints != null && zone.spawnPoints.Count > 0)
        {
            int safeIndex = Mathf.Min(index, zone.spawnPoints.Count - 1);
            return zone.spawnPoints[safeIndex].position;
        }

        return zone.triggerCollider.transform.position;
    }
}

public class AisleTrigger : MonoBehaviour
{
    private AisleEnemyManager manager;
    private TriggerZone zoneData;

    public void Setup(AisleEnemyManager manager, TriggerZone zoneData)
    {
        this.manager = manager;
        this.zoneData = zoneData;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            manager.ActivateZone(zoneData);
        }
    }
}
