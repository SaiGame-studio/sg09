using UnityEngine;
using UnityEngine.AI;

public class WorkerMovement : SaiBehaviour
{
    [SerializeField] protected Transform target;
    [SerializeField] protected NavMeshAgent navMeshAgent;
    [SerializeField] protected Animator animator;
    [SerializeField] protected bool isWalking = false;
    [SerializeField] protected bool isWorking = false;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadAgent();
        this.LoadAnimator();
    }

    protected override void FixedUpdate()
    {
        this.FindHouse();
        this.Moving();
        this.Animating();
    }

    protected virtual void LoadAgent()
    {
        if (this.navMeshAgent != null) return;
        this.navMeshAgent = GetComponent<NavMeshAgent>();
        Debug.Log(transform.name + ": LoadAgent", gameObject);
    }

    protected virtual void LoadAnimator()
    {
        if (this.animator != null) return;
        this.animator = GetComponentInChildren<Animator>();
        Debug.Log(transform.name + ": LoadAnimator", gameObject);
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
            this.isWalking = false;
            return;
        }

        this.isWalking = true;
        this.navMeshAgent.isStopped = false;
        this.navMeshAgent.SetDestination(this.target.position);
    }

    protected virtual void Animating()
    {
        this.animator.SetBool("isWalking", this.isWalking);
        this.animator.SetBool("isWorking", this.isWorking);
    }

    protected virtual void FindHouse()
    {
        BuildingCtrl buildingCtrl = BuildingManager.instance.FindBuilding(transform);
        if (buildingCtrl == null) return;
        this.target = buildingCtrl.door;
    }
}
