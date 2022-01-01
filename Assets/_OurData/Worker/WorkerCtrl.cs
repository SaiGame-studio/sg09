using UnityEngine;
using UnityEngine.AI;

public class WorkerCtrl : SaiBehaviour
{
    public WorkerBuildings workerBuildings;
    public WorkerMovement workerMovement;
    public WorkerTasks workerTasks;
    public Animator animator;
    public Transform workerModel;
    public NavMeshAgent navMeshAgent;
    public ResCarrier resCarrier;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadWorkerBuildings();
        this.LoadWorkerMovement();
        this.LoadAnimator();
        this.LoadWorkerTasks();
        this.LoadAgent();
        this.LoadResCarrier();
    }

    protected virtual void LoadWorkerTasks()
    {
        if (this.workerTasks != null) return;
        this.workerTasks = GetComponent<WorkerTasks>();
        Debug.Log(transform.name + ": LoadWorkerTasks", gameObject);
    }

    protected virtual void LoadAnimator()
    {
        if (this.animator != null) return;
        this.animator = GetComponentInChildren<Animator>();
        this.workerModel = this.animator.transform;
        Debug.Log(transform.name + ": LoadAnimator", gameObject);
    }

    protected virtual void LoadWorkerBuildings()
    {
        if (this.workerBuildings != null) return;
        this.workerBuildings = GetComponent<WorkerBuildings>();
        Debug.Log(transform.name + ": LoadWorkerBuildings", gameObject);
    }

    protected virtual void LoadWorkerMovement()
    {
        if (this.workerMovement != null) return;
        this.workerMovement = GetComponent<WorkerMovement>();
        Debug.Log(transform.name + ": LoadWorkerMovement", gameObject);
    }

    protected virtual void LoadAgent()
    {
        if (this.navMeshAgent != null) return;
        this.navMeshAgent = GetComponent<NavMeshAgent>();
        this.navMeshAgent.speed = 2f;
        Debug.Log(transform.name + ": LoadAgent", gameObject);
    }

    protected virtual void LoadResCarrier()
    {
        if (this.resCarrier != null) return;
        this.resCarrier = GetComponent<ResCarrier>();
        Debug.Log(transform.name + ": ResCarrier", gameObject);
    }
}
