using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstuctionTree : AbsConstructFromPool<TreeCtrl>
{
    protected override void LoadSpawner()
    {
        if (this.spawner != null) return;
        this.spawner = GameObject.Find("TreeSpawner").GetComponent<TreeSpawner>();
        Debug.LogWarning(transform.name + ": Load TreeSpawner Spawner", gameObject);
    }
}
