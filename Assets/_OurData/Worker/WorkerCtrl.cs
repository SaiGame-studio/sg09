using UnityEngine;

public class WorkerCtrl : SaiBehaviour
{
    public WorkerBuildings workerBuildings;
    public WorkerMovement workerMovement;
    public Animator animator;
    public Transform workerModel;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadWorkerBuildings();
        this.LoadWorkerMovement();
        this.LoadAnimator();
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
}
