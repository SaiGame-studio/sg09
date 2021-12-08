using UnityEngine;
using UnityEngine.AI;

public class WorkerMovement : SaiBehaviour
{
    [SerializeField] protected Transform target;
    [SerializeField] protected NavMeshAgent navMeshAgent;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadAgent();
    }

    protected override void FixedUpdate()
    {
        this.Moving();
    }

    protected virtual void LoadAgent()
    {
        if (this.navMeshAgent != null) return;
        this.navMeshAgent = GetComponent<NavMeshAgent>();
        Debug.Log(transform.name + ": LoadAgent", gameObject);
    }

    public virtual void SetTarget(Transform trans)
    {
        this.target = trans;
    }

    protected virtual void Moving()
    {
        if (this.target == null)
        {
            this.navMeshAgent.isStopped = true;
            return;
        }

        this.navMeshAgent.isStopped = false;
        this.navMeshAgent.SetDestination(this.target.position);
    }
}
