using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warehouse : SaiBehaviour
{
    public BuildingType buildingType;
    [SerializeField] protected List<ResHolder> resHolders;

    protected override void LoadComponents()
    {
        this.LoadHolders();
    }

    protected virtual void LoadHolders()
    {
        if (this.resHolders.Count > 0) return;

        Transform res = transform.Find("Res");
        foreach (Transform resTran in res)
        {
            Debug.Log(resTran.name);
            ResHolder resHolder = resTran.GetComponent<ResHolder>();
            if (resHolder == null) continue;
            this.resHolders.Add(resHolder);
        }

        Debug.Log(transform.name + ": LoadHolders");
    }

    public virtual ResHolder GetHolder(ResourceName name)
    {
        return this.resHolders.Find((holder) => holder.Name() == name);
    }
}
