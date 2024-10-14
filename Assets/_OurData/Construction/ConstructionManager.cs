using System.Collections.Generic;
using UnityEngine;

public class ConstructionManager : SaiSingleton<ConstructionManager>
{
    [SerializeField] protected List<AbsConstruction> constructions;

    protected virtual void FixedUpdate()
    {
        this.ConstructionCleaning();
    }

    public virtual void Add(AbsConstruction abstractConstruction)
    {
        this.constructions.Add(abstractConstruction);
    }

    public virtual void Remove(AbsConstruction abstractConstruction)
    {
        this.constructions.Remove(abstractConstruction);
    }

    protected virtual void ConstructionCleaning()
    {
        if (this.constructions.Count < 1) return;

        AbsConstruction abstractConstruction;
        for (int i = 0;i<this.constructions.Count;i++)
        {
            abstractConstruction = this.constructions[i];
            if (abstractConstruction == null) this.constructions.RemoveAt(i);
        }
    }

    public virtual AbsConstruction GetConstruction()
    {
        foreach (AbsConstruction construction in this.constructions)
        {
            if (construction.Builder != null) continue;
            if (!construction.HasEnoughResource()) return construction;
        }

        return null;
    }
}
