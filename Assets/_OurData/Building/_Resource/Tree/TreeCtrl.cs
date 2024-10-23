using UnityEngine;

public abstract class TreeCtrl : ResourceCtrl
{
    [Header("Tree")]
    [SerializeField] protected LogwoodGenerator logwoodGenerator;

    public LogwoodGenerator LogwoodGenerator => logwoodGenerator;

    [SerializeField] protected TreeLevel treeLevel;
    public TreeLevel TreeLevel => treeLevel;

    public WorkerCtrl choper;

    protected override void OnEnable()
    {
        base.OnEnable();
        this.Reborn();
    }

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

    protected virtual void Reborn()
    {
        this.treeLevel.enabled = true;
    }
}
