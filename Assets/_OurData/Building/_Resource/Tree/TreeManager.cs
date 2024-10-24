using System.Collections.Generic;
using UnityEngine;

public class TreeManager : SaiSingleton<TreeManager>
{
    [SerializeField] protected TreeSpawnerCtrl ctrl;
    [SerializeField] protected List<TreeCtrl> trees = new();
    public List<TreeCtrl> Trees => trees;

    protected virtual void FixedUpdate()
    {
        this.Growing();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadTreeSpawnerCtrl();
        this.LoadTrees();
    }

    protected virtual void LoadTreeSpawnerCtrl()
    {
        if (this.ctrl != null) return;
        this.ctrl = GetComponent<TreeSpawnerCtrl>();
        Debug.Log(transform.name + ": LoadBuildingSpawnerCtrl", gameObject);
    }

    protected virtual void LoadTrees()
    {
        if (this.trees.Count > 0) return;
        TreeCtrl[] trees = this.ctrl.Spawner.PoolHolder.GetComponentsInChildren<TreeCtrl>();
        this.trees = new List<TreeCtrl>(trees);
        //Debug.Log(transform.name + ": LoadBuildingSpawnerCtrl", gameObject);
    }

    public virtual void Add(TreeCtrl treeCtrl)
    {
        this.trees.Add(treeCtrl);
    }

    public virtual void Remove(TreeCtrl treeCtrl)
    {
        this.trees.Remove(treeCtrl);
    }

    protected virtual void Growing()
    {
        foreach(TreeCtrl treeCtrl in this.trees)
        {
            treeCtrl.TreeLevel.Growing();
            treeCtrl.LogwoodGenerator.Generating();
        }
    }
}
