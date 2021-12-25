using UnityEngine;
using UnityEngine.AI;

public class WorkerMovement : SaiBehaviour
{
    public WorkerCtrl workerCtrl;
    [SerializeField] protected Transform target;
    [SerializeField] protected bool isWalking = false;
    [SerializeField] protected bool isWorking = false;
    [SerializeField] protected float walkLimit = 0.7f;
    [SerializeField] protected float targetDistance = 0f;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadWorkerCtrl();
    }

    protected override void FixedUpdate()
    {
        this.Moving();
        this.Animating();
    }

    protected virtual void LoadWorkerCtrl()
    {
        if (this.workerCtrl != null) return;
        this.workerCtrl = GetComponent<WorkerCtrl>();
        Debug.Log(transform.name + ": LoadWorkerCtrl", gameObject);
    }


    public virtual Transform GetTarget()
    {
        return this.target;
    }

    public virtual void SetTarget(Transform trans)
    {
        this.target = trans;
    }

    protected virtual void Moving()
    {
        if (this.target == null || this.IsClose2Target())
        {
            this.workerCtrl.navMeshAgent.isStopped = true;
            this.isWalking = false;
            return;
        }

        this.isWalking = true;
        this.workerCtrl.navMeshAgent.isStopped = false;
        this.workerCtrl.navMeshAgent.SetDestination(this.target.position);
    }
        
    public virtual bool IsClose2Target()
    {
        Vector3 targetPos = this.target.position;
        targetPos.y = transform.position.y;

        this.targetDistance = Vector3.Distance(transform.position, targetPos);
        return this.targetDistance < this.walkLimit;
    }

    protected virtual void Animating()
    {
        this.workerCtrl.animator.SetBool("isWalking", this.isWalking);
        this.workerCtrl.animator.SetBool("isWorking", this.isWorking);
    }
}
