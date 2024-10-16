using UnityEngine;
using UnityEngine.AI;

public class WorkerMovement : SaiBehaviour
{
    public WorkerCtrl workerCtrl;
    [SerializeField] protected Transform target;
    public bool isWalking = false;
    public bool isWorking = false;
    public WorkingType workingType = WorkingType.lumberjack;
    [SerializeField] protected float walkLimit = 0.7f;
    [SerializeField] protected float targetDistance = 0f;

    protected virtual void FixedUpdate()
    {
        this.Moving();
        this.Animating();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadWorkerCtrl();
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

        if(this.target == null)
        {
           this.ActiveAgent(false);
        }
        else
        {
            this.ActiveAgent(true);
            this.IsCloseToTarget();
        }
    }

    public virtual void ActiveAgent(bool status)
    {
        this.workerCtrl.navMeshAgent.enabled = status;
    }

    protected virtual void Moving()
    {
        if (this.target == null || this.IsCloseToTarget())
        {
            this.isWalking = false;
            return;
        }

        //TODO: only set new destination when target change
        this.isWalking = true;
        this.workerCtrl.navMeshAgent.SetDestination(this.target.position);
    }

    public virtual bool CanReachDestination(Vector3 targetPosition)
    {
        NavMeshPath path = new();
        this.workerCtrl.workerMovement.ActiveAgent(true);
        this.workerCtrl.navMeshAgent.CalculatePath(targetPosition, path);
        this.workerCtrl.workerMovement.ActiveAgent(false);

        return path.status == NavMeshPathStatus.PathComplete;
    }

    public virtual bool IsCloseToTarget()
    {
        if (this.target == null) return false;

        Vector3 targetPos = this.target.position;
        targetPos.y = transform.position.y;

        this.targetDistance = Vector3.Distance(transform.position, targetPos);
        return this.targetDistance < this.walkLimit;
    }

    protected virtual void Animating()
    {
        this.workerCtrl.animator.SetBool("isWalking", this.isWalking);
        this.workerCtrl.animator.SetBool("isWorking", this.isWorking);
        this.workerCtrl.animator.SetFloat("workingType", (float)this.workingType);
    }

    public virtual float TargetDistance()
    {
        return this.targetDistance;
    }
}
