using UnityEngine;

public class TreeCtrl : SaiBehaviour
{
    public TreeLevel treeLevel;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadTreeLevel();
    }

    protected virtual void LoadTreeLevel()
    {
        if (this.treeLevel != null) return;
        this.treeLevel = GetComponent<TreeLevel>();
        Debug.Log(transform.name + " LoadTreeLevel", gameObject);
    }
}
