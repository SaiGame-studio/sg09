using System.Collections.Generic;
using UnityEngine;

public class TreeManager : SaiBehaviour
{
    public static TreeManager instance;
    [SerializeField] protected List<GameObject> trees;

    protected override void Awake()
    {
        base.Awake();
        if (TreeManager.instance != null) Debug.LogError("Only 1 TreeManager allow");
        TreeManager.instance = this;
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadTrees();
    }

    protected virtual void LoadTrees()
    {
        if (this.trees.Count > 0) return;
        foreach(Transform tree in transform)
        {
            this.TreeAdd(tree.gameObject);
        }

        Debug.Log(transform.name + ": LoadTrees", gameObject);
    }

    public virtual void TreeAdd(GameObject tree)
    {
        if (this.trees.Contains(tree)) return;
        this.trees.Add(tree);
        tree.transform.parent = transform;
    }

    public virtual List<GameObject> Trees()
    {
        return this.trees;
    }
}
