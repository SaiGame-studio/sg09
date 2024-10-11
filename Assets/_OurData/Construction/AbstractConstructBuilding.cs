using UnityEngine;

public abstract class AbstractConstructBuilding : AbstractPoolConstruct<BuildingCtrl>
{
    protected override void LoadSpawner()
    {
        if (this.spawner != null) return;
        this.spawner = GameObject.Find("BuildingSpawner").GetComponent<BuildingSpawner>();
        Debug.LogWarning(transform.name + ": Load ForestHut Spawner", gameObject);
    }

    protected override Transform FinishBuild()
    {
        Transform newBuild = base.FinishBuild();
        BuildingCtrl buildingCtrl = newBuild.GetComponent<BuildingCtrl>();
        BuildingSpawnerCtrl.Instance.Manager.AddBuilding(buildingCtrl);
        return newBuild;
    }
}
