using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public abstract class AbstractConstruction : SaiBehaviour
{
    [Header("AbstractConstruction")]
    public BuildingCtrl builder;
    [SerializeField] protected ConstructionCtrl constructionCtrl;
    public bool isPlaced= false;
    [SerializeField] protected float percent = 0f;
    [SerializeField] protected float timer = 0f;
    [SerializeField] protected float delay = 0.001f;
    [SerializeField] protected List<Resource> resRequires;
    [SerializeField] protected List<Resource> resHave;

    protected virtual void FixedUpdate()
    {
        this.Building();
        if (this.percent >= 100f)  this.FinishBuild();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        this.BuildReset();
    }

    protected abstract Transform CreateBuild();
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
            Resource resHas = this.resHave.Find((x) => x.name == resRequire.name);
            if (resHas == null) return resRequire.name;
            if (resRequire.number > resHas.number) return resRequire.name;
        }

        return ResourceName.noResource;
    }

    protected virtual Transform FinishBuild()
    {
        Transform newBuild = this.CreateBuild();

        this.DestroyContruction();
        return newBuild;
    }

    protected virtual void DestroyContruction()
    {
        ConstructionManager.Instance.Remove(this);
        this.constructionCtrl.Despawn.DoDespawn();
    }

    protected virtual void BuildReset()
    {
        this.percent = 0;
        this.timer = 0;
        this.isPlaced = false;
    }

    public virtual void AddRes(ResourceName resourceName, float count)
    {
        Resource resource = this.resHave.Find((x) => x.name == resourceName);
        if (resource != null)
        {
            resource.number += count;
            return;
        }

        resource = new Resource
        {
            name = resourceName,
            number = count
        };
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

    public virtual string GetConstructionName()
    {
        return transform.name;
    }
}
