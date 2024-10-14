using System.Collections.Generic;
using UnityEngine;

public class TreeManager : SaiBehaviour
{
    [SerializeField] protected TreeSpawnerCtrl ctrl;
    [SerializeField] protected List<TreeCtrl> trees = new();
    public List<TreeCtrl> Trees => trees;

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
}
