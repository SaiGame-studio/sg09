using UnityEngine;

public abstract class Despawn<T> : DespawnBase where T : PoolObj
{
    [SerializeField] protected T parent;
    [SerializeField] protected Spawner<T> spawner;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadParent();
        this.LoadSpawner();
    }

    protected virtual void LoadParent()
    {
        if (this.parent != null) return;
        this.parent = transform.parent.GetComponent<T>();
        Debug.LogWarning(transform.name + ": LoadParent", gameObject);
    }

    protected virtual void LoadSpawner()
    {
        if (this.spawner != null) return;
        this.spawner = FindAnyObjectByType<Spawner<T>>();
        Debug.LogWarning(transform.name + ": LoadSpawner", gameObject);
    }


    public override void DoDespawn()
    {
        this.spawner.Despawn(this.parent);
    }

    public virtual void DelayDespawn(float delay)
    {
        Invoke(nameof(this.DoDespawn), delay);
    }
}
