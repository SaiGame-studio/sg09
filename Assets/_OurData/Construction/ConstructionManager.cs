using System.Collections.Generic;
using UnityEngine;

public class ConstructionManager : SaiSingleton<ConstructionManager>
{
    [SerializeField] protected ConstructionSpawnerCtrl ctrl;
    [SerializeField] protected List<AbsConstruction> constructions;
    public List<AbsConstruction> Constructions => constructions;

    protected virtual void FixedUpdate()
    {
        this.ConstructionCleaning();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadConstructionSpawnerCtrl();
        this.LoadPoolObjects();
    }

    protected virtual void LoadConstructionSpawnerCtrl()
    {
        if (this.ctrl != null) return;
        this.ctrl = GetComponent<ConstructionSpawnerCtrl>();
        Debug.Log(transform.name + ": LoadConstructionSpawnerCtrl", gameObject);
    }

    protected virtual void LoadPoolObjects()
    {
        if (this.constructions.Count > 0) return;
        AbsConstruction[] components = this.ctrl.Spawner.PoolHolder.GetComponentsInChildren<AbsConstruction>();
        this.constructions = new List<AbsConstruction>(components);
        //Debug.Log(transform.name + ": LoadPoolObjects", gameObject);
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
