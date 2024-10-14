using System.Collections.Generic;
using UnityEngine;

public abstract class AbsConstruction : SaiBehaviour
{
    [Header("Abs Construction")]
    [SerializeField] protected BuildingCtrl builder;
    public BuildingCtrl Builder => builder;
    [SerializeField] protected ConstructionCtrl constructionCtrl;
    [SerializeField] protected bool isPlaced = false;
    [SerializeField] protected bool isFinish = false;
    [SerializeField] protected float percent = 0f;
    [SerializeField] protected float timer = 0f;
    [SerializeField] protected float delay = 0.001f;
    [SerializeField] protected List<Resource> resRequires;
    [SerializeField] protected List<Resource> resHave;

    protected virtual void FixedUpdate()
    {
        this.Building();
        this.FinishBuild();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        this.Reborn();
    }

    protected abstract void CreateBuild();
    protected abstract string GetBuildName();

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadConstructionCtrl();
        this.LoadResRequires();
    }

    protected virtual void LoadConstructionCtrl()
    {
        if (this.constructionCtrl != null) return;
        this.constructionCtrl = GetComponent<ConstructionCtrl>();
        Debug.Log(transform.name + ": LoadConstructionCtrl", gameObject);
    }


    protected virtual void Building()
    {
        if (!this.isPlaced) return;
        if (!this.HasEnoughResource()) return;

        this.timer += Time.fixedDeltaTime;
        if (this.timer < this.delay) return;
        this.timer = 0;
        this.percent += 1;
    }

    protected virtual void LoadResRequires()
    {
        //For override
    }

    public virtual bool HasEnoughResource()
    {
        if (this.resRequires.Count < 1) return true;

        foreach (Resource resRequire in this.resRequires)
        {
            Resource resHas = this.resHave.Find((x) => x.name == resRequire.name);
            if (resHas == null) return false;
            if (resRequire.number > resHas.number) return false;
        }

        return true;
    }

    public virtual ResourceName GetResRequireName()
    {
        foreach (Resource resRequire in this.resRequires)
        {
            Resource resHas = this.resHave.Find((resource) => resource.codeName == resRequire.codeName);
            if (resHas == null) return resRequire.codeName;
            if (resRequire.number > resHas.number) return resRequire.codeName;
        }

        return ResourceName.noResource;
    }

    protected virtual void FinishBuild()
    {
        if (this.isFinish) return;
        if (this.percent < 100f) return;
        this.CreateBuild();
        this.DestroyContruction();
        this.isFinish = true;
    }

    protected virtual void DestroyContruction()
    {
        ConstructionManager.Instance.Remove(this);
        this.constructionCtrl.Despawn.DoDespawn();
    }

    protected virtual void Reborn()
    {
        this.builder = null;
        this.percent = 0;
        this.timer = 0;
        this.isPlaced = false;
        this.isFinish = false;
        this.resHave = new();
    }

    public virtual void AddRes(ResourceName resourceName, int count)
    {
        Resource resource = this.resHave.Find((x) => x.codeName == resourceName);
        if (resource != null)
        {
            resource.number += count;
            return;
        }

        resource = new Resource(resourceName, count);
        this.resHave.Add(resource);
    }

    public virtual float Percent()
    {
        return this.percent;
    }

    public virtual void Finish()
    {
        this.percent = 100;
    }

    public virtual void SetIsPlaced(bool status)
    {
        this.isPlaced = status;
    }

    public virtual void SetBuilder(BuildingCtrl builder)
    {
        this.builder = builder;
    }
}
