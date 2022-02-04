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

    public virtual void AddConstruction(AbstractConstruction abstractConstruction)
    {
        this.constructions.Add(abstractConstruction);
        abstractConstruction.transform.parent = transform;
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
