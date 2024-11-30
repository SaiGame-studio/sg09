using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTrees : SaiBehaviour
{
    [SerializeField] protected int spawnRadius = 500;
    [SerializeField] protected int spawnJunk = 5;
    [SerializeField] protected int spawnCount = 10000;

    protected virtual void FixedUpdate()
    {
        this.Spawning();
    }

    protected virtual void Spawning()
    {
        if (TreeManager.Instance.Trees.Count >= this.spawnCount) return;

        Vector3 position;
        TreeCtrl newTree;
        TreeCtrl prefab = TreeSpawnerCtrl.Instance.Spawner.PoolPrefabs.GetByName("Tree_1");
        for (int i = 0; i < this.spawnJunk; i++)
        {
            position = new();
            position.x = Random.Range(-this.spawnRadius, this.spawnRadius);
            position.z = Random.Range(-this.spawnRadius, this.spawnRadius);
            newTree = TreeSpawnerCtrl.Instance.Spawner.Spawn(prefab, position);
            newTree.SetActive(true);
        }
    }
}
