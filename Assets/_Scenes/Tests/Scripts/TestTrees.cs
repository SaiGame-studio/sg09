using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTrees : SaiBehaviour
{
    [SerializeField] protected int spawnJunk = 5;
    [SerializeField] protected int spawnCount = 10000;

    protected virtual void FixedUpdate()
    {
        this.Spawning();
    }

    protected virtual void Spawning()
    {
        if (TreeManager.Instance.Trees.Count >= this.spawnCount) return;

        TreeCtrl newTree;
        TreeCtrl prefab = TreeSpawnerCtrl.Instance.Spawner.PoolPrefabs.GetByName("Tree_1");
        for (int i = 0; i < 50; i++)
        {
            newTree = TreeSpawnerCtrl.Instance.Spawner.Spawn(prefab);
            newTree.SetActive(true);
        }
    }
}
