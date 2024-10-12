using UnityEngine;

public class BuildingSpawner : Spawner<BuildingCtrl>
{
    [SerializeField] protected BuildingSpawnerCtrl ctrl;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadBuildingSpawnerCtrl();
    }

    protected virtual void LoadBuildingSpawnerCtrl()
    {
        if (this.ctrl != null) return;
        this.ctrl = GetComponent<BuildingSpawnerCtrl>();
        Debug.Log(transform.name + ": LoadBuildingSpawnerCtrl", gameObject);
    }

    public override BuildingCtrl Spawn(BuildingCtrl prefab)
    {
        BuildingCtrl newObj = base.Spawn(prefab);
        this.ctrl.Manager.Add(newObj);
        return newObj;
    }
}
