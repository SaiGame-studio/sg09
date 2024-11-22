using UnityEngine;
using UnityEngine.AI;

public class WorkerMovement : SaiBehaviour
{
    public WorkerCtrl workerCtrl;
    [SerializeField] protected Transform target;
    [SerializeField] protected bool isWalking = false;

    [SerializeField] protected bool isWorking = false;
    public bool IsWorking => isWorking;

    [SerializeField] protected WorkingType workingType = WorkingType.lumberjack;
    [SerializeField] protected float walkLimit = 0.7f;
    [SerializeField] protected float targetDistance = 0f;

    //protected virtual void FixedUpdate()
    //{
    //    this.Moving();
    //    this.Animating();
    //}

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadWorkerCtrl();
    }

    public virtual void SetWorkingType(WorkingType workingType)
    {
        this.workingType = workingType;
        this.workerCtrl.animator.SetFloat("workingType", (float)this.workingType);
    }

    public virtual void SetWalking(bool status)
    {
        this.isWalking = status;
        this.UpdateWalkingAnimation();
    }

    public virtual void UpdateWalkingAnimation()
    {
        this.workerCtrl.animator.SetBool("isWalking", this.isWalking);
    }

    public virtual void SetWorking(bool status)
    {
        this.isWorking = status;
        this.workerCtrl.animator.SetBool("isWorking", this.isWorking);
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

        if (this.target == null)
        {
            this.ActiveAgent(false);
            this.SetWalking(false);
        }
        else if (!this.IsCloseToTarget())
        {
            this.ActiveAgent(true);
            this.workerCtrl.navMeshAgent.SetDestination(this.target.position);
            this.SetWalking(true);
        }
    }

    public virtual void ActiveAgent(bool status)
    {
        this.workerCtrl.navMeshAgent.enabled = status;
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

    public virtual float TargetDistance()
    {
        return this.targetDistance;
    }
}
