using UnityEngine;

public abstract class TreeCtrl : ResourceCtrl
{
    [Header("Tree")]
    public LogwoodGenerator logwoodGenerator;
    public TreeLevel treeLevel;
    public WorkerCtrl choper;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadTreeLevel();
        this.LoadLogwoodGenerator();
    }

    protected virtual void LoadTreeLevel()
    {
        if (this.treeLevel != null) return;
        this.treeLevel = GetComponent<TreeLevel>();
        Debug.Log(transform.name + " LoadTreeLevel", gameObject);
    }

    protected virtual void LoadLogwoodGenerator()
    {
        if (this.logwoodGenerator != null) return;
        this.logwoodGenerator = GetComponent<LogwoodGenerator>();
        Debug.Log(transform.name + " LoadLogwoodGenerator", gameObject);
    }

}
