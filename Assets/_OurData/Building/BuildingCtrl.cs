using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingCtrl : SaiBehaviour
{
    public Workers workers;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadWorkers();
    }

    protected virtual void LoadWorkers()
    {
        if (this.workers != null) return;
        this.workers = GetComponent<Workers>();
        Debug.Log(transform.name + "LoadWorkers", gameObject);
    }
}
