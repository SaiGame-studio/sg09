using UnityEngine;

public abstract class ConstructionCtrl : PoolObj
{
    [Header("Construction")]
    public LimitRadius limitRadius;
    public AbsConstruction abstractConstruction;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadLimitRadius();
        this.LoadAbstractConstruction();
    }

    protected virtual void LoadLimitRadius()
    {
        if (this.limitRadius != null) return;
        this.limitRadius = GetComponent<LimitRadius>();
        Debug.Log(transform.name + ": LoadLimitRadius", gameObject);
    }

    protected virtual void LoadAbstractConstruction()
    {
        if (this.abstractConstruction != null) return;
        this.abstractConstruction = GetComponent<AbsConstruction>();
        Debug.Log(transform.name + ": LoadAbstractConstruction", gameObject);
    }
}
