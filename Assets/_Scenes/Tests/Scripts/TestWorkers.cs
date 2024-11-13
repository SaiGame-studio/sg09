using UnityEngine;

public class TestWorkers : SaiBehaviour
{
    [SerializeField] protected int spawnJunk = 5;
    [SerializeField] protected int spawnCount = 10000;

    protected override void OnEnable()
    {
        base.OnEnable();
        InvokeRepeating(nameof(this.Spawning), 1, 1);
    }

    protected virtual void Spawning()
    {
        if (ConstructionManager.Instance.Constructions.Count >= this.spawnCount) return;

        ConstructionCtrl newObj;
        ConstructionCtrl  prefab = ConstructionSpawnerCtrl.Instance.Spawner.PoolPrefabs.GetByName("BuildWorker");
        for (int i = 0; i < 50; i++)
        {
            newObj = ConstructionSpawnerCtrl.Instance.Spawner.Spawn(prefab, transform.position);
            newObj.SetActive(true);
        }
    }
}
