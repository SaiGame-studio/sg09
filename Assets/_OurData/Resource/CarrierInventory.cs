using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarrierInventory : Warehouse
{
    [Header("Carrier")]
    [SerializeField] protected int carryCount = 1;
    public int CarryCount => carryCount;

    public virtual List<Resource> TakeAll()
    {
        List<Resource> resources = new(this.resources);
        this.resources = new List<Resource>();
        return resources;
    }

    public virtual int Taking(int number)
    {
        int taking = number;
        if (taking > this.CarryCount) taking = this.CarryCount;
        return taking;
    }
}
