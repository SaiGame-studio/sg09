using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingCtrl : SaiBehaviour
{
    public Transform door;
    public Workers workers;
    public Warehouse warehouse;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadWorkers();
        this.LoadDoor();
        this.LoadWarehouse();
    }

    protected virtual void LoadWorkers()
    {
        if (this.workers != null) return;
        this.workers = GetComponent<Workers>();
        Debug.Log(transform.name + " LoadWorkers", gameObject);
    }

    protected virtual void LoadDoor()
    {
        if (this.door != null) return;
        this.door = transform.Find("Door");
        Debug.Log(transform.name + " LoadDoor", gameObject);
    }

    protected virtual void LoadWarehouse()
    {
        if (this.warehouse != null) return;
        this.warehouse = GetComponent<Warehouse>();
        Debug.Log(transform.name + " LoadWarehouse", gameObject);
    }
}
