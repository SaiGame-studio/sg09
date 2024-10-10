using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeSpawnerCtrl : SaiSingleton<TreeSpawnerCtrl>
{
    [SerializeField] protected TreeSpawner spawner;
    public TreeSpawner Spawner => spawner;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadTreeSpawner();
    }

    protected virtual void LoadTreeSpawner()
    {
        if (this.spawner != null) return;
        this.spawner = GetComponent<TreeSpawner>();
        Debug.Log(transform.name + " LoadTreeSpawner", gameObject);
    }
}
