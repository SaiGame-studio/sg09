using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingTask : SaiBehaviour
{
    public BuildingCtrl buildingCtrl;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadBuildingCtrl();
    }

    protected virtual void LoadBuildingCtrl()
    {
        if (this.buildingCtrl != null) return;
        this.buildingCtrl = GetComponent<BuildingCtrl>();
        Debug.Log(transform.name + " LoadBuildingCtrl", gameObject);
    }

    public virtual void DoingTask()
    {
        //For override
    }
}