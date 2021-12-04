using UnityEngine;

public class TreeLevel : BuildLevel
{
    [SerializeField] protected Tree tree;
    [SerializeField] protected float treeTimer = 0;
    [SerializeField] protected float treeDelay = Mathf.Infinity;

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        this.Growing();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadTree();
    }

    protected virtual void LoadTree()
    {
        if (this.tree != null) return;
        this.tree = GetComponent<Tree>();
        this.GetTreeDelay();
        Debug.Log(transform.name + ": LoadTree");
    }

    protected virtual void GetTreeDelay()
    {
        int levelCount = this.levels.Count - 2;
        this.treeDelay = this.tree.GetCreateDelay() / levelCount;
    }

    protected virtual void Growing()
    {
        this.treeTimer += Time.fixedDeltaTime;
        if (this.treeTimer < this.treeDelay) return;
        this.treeTimer = 0;

        this.ShowNextBuild();
    }
}
