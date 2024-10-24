using UnityEngine;

public abstract class DespawnByTime<T> : Despawn<T>  where T : PoolObj
{
    [SerializeField] protected bool isDespawnByTime = false;
    [SerializeField] protected float timeLife = 7f;
    [SerializeField] protected float currentTime = 7f;

    protected virtual void FixedUpdate()
    {
        this.Despawning();
    }

    protected virtual void Despawning()
    {
        if (!this.isDespawnByTime) return;

        this.currentTime -= Time.fixedDeltaTime;
        if (this.currentTime > 0) return;

        this.DoDespawn();
        this.currentTime = this.timeLife;
    }
}
