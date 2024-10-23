using UnityEngine;

public class TreeLevel : BuildLevel
{
    [SerializeField] protected bool isMaxLevel = false;
    [SerializeField] protected LogwoodGenerator tree;
    [SerializeField] protected float treeTimer = 0;
    [SerializeField] protected float treeDelay = Mathf.Infinity;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadTree();
    }

    protected virtual void LoadTree()
    {
        if (this.tree != null) return;
        this.tree = GetComponent<LogwoodGenerator>();
        this.GetTreeDelay();
        Debug.Log(transform.name + ": LoadTree");
    }

    protected virtual void GetTreeDelay()
    {
        int levelCount = this.levels.Count - 2;
        this.treeDelay = this.tree.GetCreateDelay() / levelCount;
    }

    public virtual void Growing()
    {
        if (this.IsMaxLevel()) this.enabled = false;
        this.treeTimer += this.GetElapsedTime();
        if (this.treeTimer < this.treeDelay) return;
        this.treeTimer = 0;

        this.ShowNextBuild();
    }

    public virtual bool IsMaxLevel()
    {
        if (this.currentLevel == this.levels.Count - 2) this.isMaxLevel = true;
        else this.isMaxLevel = false;
        return this.isMaxLevel;
    }
}
