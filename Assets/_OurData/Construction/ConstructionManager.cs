using System.Collections.Generic;
using UnityEngine;

public class ConstructionManager : SaiSingleton<ConstructionManager>
{
    [SerializeField] protected List<AbstractConstruction> constructions;

    protected virtual void FixedUpdate()
    {
        this.ConstructionCleaning();
    }

    public virtual void Add(AbstractConstruction abstractConstruction)
    {
        this.constructions.Add(abstractConstruction);
    }

    public virtual void Remove(AbstractConstruction abstractConstruction)
    {
        this.constructions.Remove(abstractConstruction);
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
