using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : SaiBehaviour
{
    public static BuildingManager instance;
    [SerializeField] protected List<BuildingCtrl> buildingCtrls;

    protected override void Awake()
    {
        base.Awake();
        if (BuildingManager.instance != null) Debug.LogError("Only 1 BuildingManager allow");
        BuildingManager.instance = this;
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadBuildingCtrls();
    }

    protected virtual void LoadBuildingCtrls()
    {
        if (this.buildingCtrls.Count > 0) return;
        foreach (Transform child in transform)
        {
            BuildingCtrl ctrl = child.GetComponent<BuildingCtrl>();
            if (ctrl == null) continue;
            this.buildingCtrls.Add(ctrl);
        }

        Debug.Log(transform.name + "LoadBuildingCtrls", gameObject);
    }

    public virtual BuildingCtrl FindBuilding(Transform worker)
    {
        BuildingCtrl buildingCtrl;
        for (int i = 0; i < this.buildingCtrls.Count; i++)
        {
            buildingCtrl = this.buildingCtrls[i];
            if (!buildingCtrl.workers.IsNeedWorker()) continue;

            buildingCtrl.workers.AddWorker(worker);
            return buildingCtrl;
        }
        return null;
    }
}
