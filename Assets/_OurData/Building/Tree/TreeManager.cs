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
    }

    protected virtual void LoadTreeSpawnerCtrl()
    {
        if (this.ctrl != null) return;
        this.ctrl = GetComponent<TreeSpawnerCtrl>();
        Debug.Log(transform.name + ": LoadBuildingSpawnerCtrl", gameObject);
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
