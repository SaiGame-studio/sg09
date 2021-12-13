using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingCtrl : SaiBehaviour
{
    public Transform door;
    public Workers workers;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadWorkers();
        this.LoadDoor();
    }

    protected virtual void LoadWorkers()
    {
        if (this.workers != null) return;
        this.workers = GetComponent<Workers>();
        Debug.Log(transform.name + "LoadWorkers", gameObject);
    }

    protected virtual void LoadDoor()
    {
        if (this.door != null) return;
        this.door = transform.Find("Door");
        Debug.Log(transform.name + "LoadDoor", gameObject);
    }
}
