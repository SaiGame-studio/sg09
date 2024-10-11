using UnityEngine;

public class BuildingSpawnerCtrl : SaiSingleton<BuildingSpawnerCtrl>
{
    [SerializeField] protected BuildingManager manager;
    public BuildingManager Manager => manager;

    [SerializeField] protected BuildingSpawner spawner;
    public BuildingSpawner Spawner => spawner;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadBuildingManager();
        this.LoadBuildingSpawner();
    }

    protected virtual void LoadBuildingManager()
    {
        if (this.manager != null) return;
        this.manager = GetComponent<BuildingManager>();
        Debug.Log(transform.name + ": LoadBuildingManager", gameObject);
    }

    protected virtual void LoadBuildingSpawner()
    {
        if (this.spawner != null) return;
        this.spawner = GetComponent<BuildingSpawner>();
        Debug.Log(transform.name + ": LoadBuildingSpawner", gameObject);
    }
}
