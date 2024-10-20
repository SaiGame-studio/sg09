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
}
