using UnityEngine;

public abstract class AbstractPoolConstruct<T> : AbstractConstruction where T : PoolObj
{
    [Header("AbstractPoolConstruct")]
    [SerializeField] protected Spawner<T> spawner;

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

    protected override Transform CreateBuild()
    {
        PoolObj prefab = this.spawner.PoolPrefabs.GetByName(this.GetBuildName());
        PoolObj newObject = this.spawner.Spawn((T)prefab, transform.position);
        newObject.gameObject.SetActive(true);
        return newObject.transform;
    }
}
