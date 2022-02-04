using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbstractConstruction : SaiBehaviour
{
    [Header("Build")]
    public BuildingCtrl builder;
    [SerializeField] protected float percent = 0f;
    [SerializeField] protected float timer = 0f;
    [SerializeField] protected float delay = 0.05f;
    [SerializeField] protected List<string> buildNames;
    [SerializeField] protected List<Resource> resRequires;
    [SerializeField] protected List<Resource> resHave;

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        this.Building();
        if (this.percent >= 100f)  this.FinishBuild();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        this.BuildReset();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadBuildNames();
        this.LoadResRequires();
    }

    protected virtual void Building()
    {
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
        Transform newBuild = PrefabManager.instance.Instantiate(this.GetBuildName());
        newBuild.position = transform.position;
        newBuild.gameObject.SetActive(true);

        PrefabManager.instance.Destroy(transform);
        return newBuild;
    }

    protected virtual string GetBuildName()
    {
        int rand = Random.Range(0, this.buildNames.Count);
        return this.buildNames[rand];
    }

    protected virtual void BuildReset()
    {
        this.percent = 0;
        this.timer = 0;
    }

    protected virtual void LoadBuildNames()
    {
        if (this.buildNames.Count > 0) return;
        string name = transform.name.Replace("Build", "");
        this.buildNames.Add(name);
        Debug.Log(transform.name + ": LoadBuildNames", gameObject);
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
}
