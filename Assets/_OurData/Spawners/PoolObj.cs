using UnityEngine;

public abstract class PoolObj : SaiBehaviour
{
    [Header("PoolObj")]
    [SerializeField] protected DespawnBase despawn;
    public DespawnBase Despawn => despawn;

    public abstract string GetName();

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadDespawn();
    }

    protected virtual void LoadDespawn()
    {
        if (this.despawn != null) return;
        this.despawn = transform.GetComponentInChildren<DespawnBase>();
        Debug.LogWarning(transform.name + ": LoadDespawn", gameObject);
    }
}
