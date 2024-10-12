using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeSpawner : Spawner<TreeCtrl>
{
    [SerializeField] protected TreeSpawnerCtrl ctrl;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadTreeSpawnerCtrl();
    }

    protected virtual void LoadTreeSpawnerCtrl()
    {
        if (this.ctrl != null) return;
        this.ctrl = GetComponent<TreeSpawnerCtrl>();
        Debug.Log(transform.name + ": LoadBuildingSpawnerCtrl", gameObject);
    }

    public override TreeCtrl Spawn(TreeCtrl prefab)
    {
        TreeCtrl newTree = base.Spawn(prefab);
        this.ctrl.Manager.Add(newTree);
        return newTree;
    }
}
