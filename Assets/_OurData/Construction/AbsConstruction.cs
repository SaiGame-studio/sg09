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

    [SerializeField] protected List<Resource> resourcesNeed;
    public List<Resource> ResourcesNeed => resourcesNeed;

    [SerializeField] protected List<Resource> resoursesHave;
    public List<Resource> ResoursesHave => resoursesHave;

    protected virtual void FixedUpdate()
    {
        this.Building();
        this.FinishBuild();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
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
        if (this.resourcesNeed.Count < 1) return true;

        foreach (Resource resourceNeed in this.resourcesNeed)
        {
            Resource resourceHas = this.resoursesHave.Find((x) => x.CodeName == resourceNeed.CodeName);
            if (resourceHas == null) return false;
            Debug.Log("Has: " + resourceHas.CodeName + " " + resourceHas.Number + " / Need: " + resourceNeed.CodeName + " " + resourceNeed.Number);
            if (resourceNeed.Number > resourceHas.Number) return false;
        }

        return true;
    }

    public virtual Resource GetResourceRequired()
    {
        foreach (Resource resourceNeed in this.resourcesNeed)
        {
            Resource resourceRequired = new(resourceNeed.CodeName);
            Resource resourceHas = this.resoursesHave.Find((resource) => resource.CodeName == resourceNeed.CodeName);

            if (resourceHas == null) resourceRequired.SetNumber(resourceNeed.Number);
            else resourceRequired.SetNumber(resourceNeed.Number - resourceHas.Number);

            if (resourceRequired.Number == 0) continue;

            return resourceRequired;
        }

        return null;
    }

    public virtual void WillAdd(ResourceName resourceName, int number)
    {
        this.GetResourceHas(resourceName).WillAdd(number);
    }

    public virtual Resource GetResourceHas(ResourceName codeName)
    {
        Resource resource = this.resoursesHave.Find((resource) => resource.CodeName == codeName);
        if (resource == null)
        {
            resource = new Resource(codeName);
            this.resoursesHave.Add(resource);
        }

        return resource;
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
        this.resoursesHave = new();
    }

    public virtual void AddRes(ResourceName resourceName, int count)
    {
        Resource resource = this.resoursesHave.Find((x) => x.CodeName == resourceName);
        if (resource != null)
        {
            resource.Add(count);
            resource.Added(count);
            return;
        }

        resource = new Resource(resourceName, count);
        this.resoursesHave.Add(resource);
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
