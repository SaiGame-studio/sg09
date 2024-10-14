using UnityEngine;

public class ResHolder : SaiBehaviour
{
    [Header("Res Holder")]
    [SerializeField] protected ResourceName resourceName;
    [SerializeField] public int resCurrent = 0;
    [SerializeField] public int resMax = int.MaxValue;

    protected override void LoadComponents()
    {
        this.LoadResName();
    }

    protected virtual void LoadResName()
    {
        if (this.resourceName != ResourceName.noResource) return;

        string name = transform.name;
        this.resourceName = ResNameParser.FromString(name);
        Debug.Log(transform.name + ": LoadResName");
    }

    public virtual ResourceName Name()
    {
        return this.resourceName;
    }

    public virtual float Add(int number)
    {
        this.resCurrent += number;

        //Greater then Max? it is oki
        //if (this.resCurrent > this.resMax) this.resCurrent = this.resMax;
        return this.resCurrent;
    }

    public virtual float Deduct(int number)
    {
        //TODO: fix issue less than 0 
        return this.Add(-number);
    }

    public virtual float Current()
    {
        return this.resCurrent;
    }

    public virtual int TakeAll()
    {
        int take = this.resCurrent;
        this.resCurrent = 0;
        return take;
    }

    public virtual void SetLimit(int max)
    {
        this.resMax = max;
    }

    public virtual bool IsMax()
    {
        return this.resCurrent >= this.resMax;
    }
}
