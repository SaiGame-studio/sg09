using UnityEngine;

public class TestWorkers : SaiBehaviour
{
    [SerializeField] protected int spawnJunk = 5;
    [SerializeField] protected int spawnCount = 0;
    [SerializeField] protected int spawnMax = 10000;

    protected virtual void OnEnable()
    {
        InvokeRepeating(nameof(this.Spawning), 1, 1);
    }

    protected virtual void Spawning()
    {
        if (this.spawnCount >= this.spawnMax) return;

        this.spawnCount += this.spawnJunk;
        ConstructionCtrl newObj;
        ConstructionCtrl  prefab = ConstructionSpawnerCtrl.Instance.Spawner.PoolPrefabs.GetByName("BuildWorker");
        for (int i = 0; i < this.spawnJunk; i++)
        {
            newObj = ConstructionSpawnerCtrl.Instance.Spawner.Spawn(prefab, transform.position);
            newObj.SetActive(true);
        }
    }
}
