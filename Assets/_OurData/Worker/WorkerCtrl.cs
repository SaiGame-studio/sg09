using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class WorkerCtrl : PoolObj
{
    public CapsuleCollider _collider;
    public Rigidbody _rigidbody;
    public WorkerBuildings workerBuildings;
    public WorkerMovement workerMovement;
    public WorkerTasks workerTasks;
    public Animator animator;
    public Transform workerModel;
    public NavMeshAgent navMeshAgent;
    public ResCarrier resCarrier;

    public override string GetName()
    {
        return "Worker";
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadWorkerBuildings();
        this.LoadWorkerMovement();
        this.LoadAnimator();
        this.LoadWorkerTasks();
        this.LoadAgent();
        this.LoadResCarrier();
        this.LoadCapsuleCollider();
        this.LoadRigidbody();
    }

    protected virtual void LoadCapsuleCollider()
    {
        if (this._collider != null) return;
        this._collider = GetComponent<CapsuleCollider>();
        this._collider.isTrigger = true;
        this._collider.height = 2f;
        this._collider.radius = 0.3f;
        this._collider.center = new Vector3(0, 0.8f, 0);
        Debug.LogWarning(transform.name + ": LoadCapsuleCollider", gameObject);
    }

    protected virtual void LoadRigidbody()
    {
        if (this._rigidbody != null) return;
        this._rigidbody = GetComponent<Rigidbody>();
        this._rigidbody.useGravity = false;
        Debug.LogWarning(transform.name + ": LoadCapsuleCollider", gameObject);
    }

    protected virtual void LoadWorkerTasks()
    {
        if (this.workerTasks != null) return;
        this.workerTasks = GetComponent<WorkerTasks>();
        Debug.LogWarning(transform.name + ": LoadWorkerTasks", gameObject);
    }

    protected virtual void LoadAnimator()
    {
        if (this.animator != null) return;
        this.animator = GetComponentInChildren<Animator>();
        this.workerModel = this.animator.transform;
        Debug.LogWarning(transform.name + ": LoadAnimator", gameObject);
    }

    protected virtual void LoadWorkerBuildings()
    {
        if (this.workerBuildings != null) return;
        this.workerBuildings = GetComponent<WorkerBuildings>();
        Debug.LogWarning(transform.name + ": LoadWorkerBuildings", gameObject);
    }

    protected virtual void LoadWorkerMovement()
    {
        if (this.workerMovement != null) return;
        this.workerMovement = GetComponent<WorkerMovement>();
        Debug.LogWarning(transform.name + ": LoadWorkerMovement", gameObject);
    }

    protected virtual void LoadAgent()
    {
        if (this.navMeshAgent != null) return;
        this.navMeshAgent = GetComponent<NavMeshAgent>();
        this.navMeshAgent.speed = 2f;
        Debug.LogWarning(transform.name + ": LoadAgent", gameObject);
    }

    protected virtual void LoadResCarrier()
    {
        if (this.resCarrier != null) return;
        this.resCarrier = GetComponent<ResCarrier>();
        Debug.LogWarning(transform.name + ": ResCarrier", gameObject);
    }

    public virtual void WorkerReleased()
    {
        this.workerTasks.readyForTask = false;
        this.workerTasks.taskWorking.GoOutBuilding();
        this.workerBuildings.WorkerReleased();
    }


}
