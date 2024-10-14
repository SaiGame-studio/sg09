using UnityEngine;

public abstract class BuildingAbstract : SaiBehaviour
{
    [SerializeField] protected BuildingCtrl ctrl;
    public BuildingCtrl Ctrl => ctrl;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadBuildingCtrl();
    }

    protected virtual void LoadBuildingCtrl()
    {
        if (this.ctrl != null) return;
        this.ctrl = GetComponent<BuildingCtrl>();
        Debug.LogWarning(transform.name + ": LoadBuildingCtrl", gameObject);
    }
}
