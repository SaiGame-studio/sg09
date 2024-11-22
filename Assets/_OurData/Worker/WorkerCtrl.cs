using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class WorkerCtrl : PoolObj
{
    public CapsuleCollider _collider;
    public Rigidbody _rigidbody;
    public WorkerBuildings buildings;
    public WorkerMovement workerMovement;
    public WorkerTasks workerTasks;
    public Animator animator;
    public Transform workerModel;
    public NavMeshAgent navMeshAgent;
    public CarrierInventory inventory;

    public override string GetName()
    {
        return BuildingName.Worker.ToString();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadWorkerBuildings();
        this.LoadWorkerMovement();
        this.LoadAnimator();
        this.LoadWorkerTasks();
        this.LoadAgent();
        this.LoadCarrierInventory();
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
        if (this.buildings != null) return;
        this.buildings = GetComponent<WorkerBuildings>();
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
        this.navMeshAgent.enabled = false;
        Debug.LogWarning(transform.name + ": LoadAgent", gameObject);
    }

    protected virtual void LoadCarrierInventory()
    {
        if (this.inventory != null) return;
        this.inventory = GetComponent<CarrierInventory>();
        Debug.LogWarning(transform.name + ": LoadCarrierInventory", gameObject);
    }

    public virtual void WorkerReleased()
    {
        this.workerTasks.SetReadyForTask(false);
        this.workerTasks.TaskWorking.GoOutBuilding();
        this.buildings.WorkerReleased();
    }

    public virtual void SetModelActive(bool status)
    {
        this.workerModel.gameObject.SetActive(status);

        if (status)
        {
            this.workerMovement.UpdateWalkingAnimation();
        }
    }
}
