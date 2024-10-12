using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class TreePlantPositionCtrl : EffectCtrl
{
    [SerializeField] protected SphereCollider sphereCollider;

    protected override void OnEnable()
    {
        base.OnEnable();
        this.Reborn();
    }

    public override string GetName()
    {
        return EffectName.TreePlantPosition.ToString();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadSphereCollider();
    }

    protected virtual void LoadSphereCollider()
    {
        if (this.sphereCollider != null) return;
        this.sphereCollider = GetComponent<SphereCollider>();
        this.sphereCollider.radius = 1f;
        Debug.Log(transform.name + ": LoadSphereCollider", gameObject);
    }

    protected virtual void OnTriggerEnter(Collider collider)
    {
        WorkerCtrl workerCtrl = collider.GetComponent<WorkerCtrl>();
        if (workerCtrl == null) return;
        EffectDespawn effectDespawn = (EffectDespawn)this.Despawn;
        effectDespawn.SetDespawnByTime(true);
    }

    protected virtual void Reborn()
    {
        EffectDespawn effectDespawn = (EffectDespawn)this.Despawn;
        effectDespawn.SetDespawnByTime(false);
    }
}
