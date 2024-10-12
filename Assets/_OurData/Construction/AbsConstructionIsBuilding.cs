using UnityEngine;

public abstract class AbsConstructionIsBuilding : AbsConstructFromPool<BuildingCtrl>
{
    protected override void LoadSpawner()
    {
        if (this.spawner != null) return;
        this.spawner = GameObject.Find("BuildingSpawner").GetComponent<BuildingSpawner>();
        Debug.LogWarning(transform.name + ": Load ForestHut Spawner", gameObject);
    }
}
