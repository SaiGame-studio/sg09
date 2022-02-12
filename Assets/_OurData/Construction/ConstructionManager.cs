using System.Collections.Generic;
using UnityEngine;

public class ConstructionManager : SaiBehaviour
{
    public static ConstructionManager instance;
    [SerializeField] protected List<AbstractConstruction> constructions;

    protected override void Awake()
    {
        base.Awake();
        if (ConstructionManager.instance != null) Debug.LogError("Only 1 ConstructionManager allow");
        ConstructionManager.instance = this;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        this.ConstructionCleaning();
    }

    public virtual void AddConstruction(AbstractConstruction abstractConstruction)
    {
        this.constructions.Add(abstractConstruction);
        abstractConstruction.transform.parent = transform;
    }

    protected virtual void ConstructionCleaning()
    {
        if (this.constructions.Count < 1) return;

        AbstractConstruction abstractConstruction;
        for (int i = 0;i<this.constructions.Count;i++)
        {
            abstractConstruction = this.constructions[i];
            if (abstractConstruction == null) this.constructions.RemoveAt(i);
        }
    }

    public virtual AbstractConstruction GetConstruction()
    {
        foreach (AbstractConstruction construction in this.constructions)
        {
            if (construction.builder != null) continue;
            if (!construction.HasEnoughResource()) return construction;
        }

        return null;
    }
}
