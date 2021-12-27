using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeManager : SaiBehaviour
{
    public static TreeManager instance;
    public List<GameObject> trees;

    protected override void Awake()
    {
        base.Awake();
        if (TreeManager.instance != null) Debug.LogError("Only 1 TreeManager allow");
        TreeManager.instance = this;
    }

    public virtual void TreeAdd(GameObject tree)
    {
        if (this.trees.Contains(tree)) return;

        this.trees.Add(tree);
        tree.transform.parent = transform;
    }
}
