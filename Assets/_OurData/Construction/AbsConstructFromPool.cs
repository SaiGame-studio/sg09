using UnityEngine;

public abstract class AbsConstructFromPool<T> : AbsConstruction where T : PoolObj
{
    [Header("Abs Construct From Pool")]
    [SerializeField] protected Spawner<T> spawner;
    [SerializeField] protected PoolObj newBuilding;

    protected abstract void LoadSpawner();

    protected override void LoadComponents()
    {
        this.LoadSpawner();
        base.LoadComponents();
    }

    protected override string GetBuildName()
    {
        int rand = Random.Range(0, this.spawner.PoolPrefabs.Prefabs.Count);
        return this.spawner.PoolPrefabs.Prefabs[rand].name;
    }

    protected override void CreateBuild()
    {
        PoolObj prefab = this.spawner.PoolPrefabs.GetByName(this.GetBuildName());
        this.newBuilding = this.spawner.Spawn((T)prefab, transform.position);
        this.newBuilding.gameObject.SetActive(true);
    }

    protected override void BuildReset()
    {
        base.BuildReset();
        this.newBuilding = null;
    }
}
