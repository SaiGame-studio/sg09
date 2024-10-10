using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructionSpawnerCtrl : SaiSingleton<ConstructionSpawnerCtrl>
{
    [SerializeField] protected ConstructionSpawner spawner;
    public ConstructionSpawner Spawner => spawner;

    [SerializeField] protected ConstructionCreator creator;
    public ConstructionCreator Creator => creator;


    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadSpawner();
        this.LoadCreator();
    }

    protected virtual void LoadSpawner()
    {
        if (this.spawner != null) return;
        this.spawner = GetComponent<ConstructionSpawner>();
        Debug.Log(transform.name + ": LoadConstructionSpawner", gameObject);
    }

    protected virtual void LoadCreator()
    {
        if (this.creator != null) return;
        this.creator = GetComponent<ConstructionCreator>();
        Debug.Log(transform.name + ": LoadCreator", gameObject);
    }
}
