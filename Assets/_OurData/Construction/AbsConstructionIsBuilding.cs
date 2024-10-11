using UnityEngine;

public abstract class AbsConstructionIsBuilding : AbsConstructFromPool<BuildingCtrl>
{
    protected override void LoadSpawner()
    {
        if (this.spawner != null) return;
        this.spawner = GameObject.Find("BuildingSpawner").GetComponent<BuildingSpawner>();
        Debug.LogWarning(transform.name + ": Load ForestHut Spawner", gameObject);
    }

    protected override void FinishBuild()
    {
        base.FinishBuild();
        if (this.newObject == null) return;
        BuildingCtrl buildingCtrl = (BuildingCtrl) this.newObject;
        BuildingSpawnerCtrl.Instance.Manager.AddBuilding(buildingCtrl);
    }
}
