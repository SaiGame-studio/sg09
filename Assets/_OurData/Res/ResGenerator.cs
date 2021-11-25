using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResGenerator : SaiBehaviour
{
    [SerializeField] protected List<ResHolder> resHolders;
    [SerializeField] protected List<Resource> resCreate;
    [SerializeField] protected List<Resource> resRequire;
    [SerializeField] protected float createTimer = 0f;
    [SerializeField] protected float createDelay = 7f;

    protected override void FixedUpdate()
    {
        this.Creating();
    }

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

    protected virtual void Creating()
    {
        this.createTimer += Time.fixedDeltaTime;
        if (this.createTimer < this.createDelay) return;
        this.createTimer = 0;

        if (!this.IsRequireEnough()) return;
        
        foreach(Resource res in this.resCreate)
        {
            ResHolder resHolder = this.GetHolder(res.name);
            resHolder.Add(res.number);
        }
    }

    protected virtual bool IsRequireEnough()
    {
        if (this.resRequire.Count < 1) return true;

        //TODO: this is not done yet
        return false;
    }

    public virtual ResHolder GetHolder(ResourceName name)
    {
        return this.resHolders.Find((holder) => holder.Name() == name);
    }

    public virtual float GetCreateDelay()
    {
        return this.createDelay;
    }
}
