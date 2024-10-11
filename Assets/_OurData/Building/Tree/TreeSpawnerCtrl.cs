using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeSpawnerCtrl : SaiSingleton<TreeSpawnerCtrl>
{
    [SerializeField] protected TreeSpawner spawner;
    public TreeSpawner Spawner => spawner;

    [SerializeField] protected TreeManager manager;
    public TreeManager Manager => manager;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadTreeSpawner();
        this.LoadTreeManager();
    }

    protected virtual void LoadTreeSpawner()
    {
        if (this.spawner != null) return;
        this.spawner = GetComponent<TreeSpawner>();
        Debug.Log(transform.name + " LoadTreeSpawner", gameObject);
    }

    protected virtual void LoadTreeManager()
    {
        if (this.manager != null) return;
        this.manager = GetComponent<TreeManager>();
        Debug.Log(transform.name + " LoadTreeManager", gameObject);
    }
}
