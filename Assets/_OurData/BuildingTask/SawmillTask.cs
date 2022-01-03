using UnityEngine;

public class SawmillTask : BuildingTask
{
    [Header("Sawmill")]
    [SerializeField] protected Transform workingPoint;
    [SerializeField] protected float workingSpeed = 7;

    protected override void Start()
    {
        base.Start();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadWorkingPoint();
    }

    protected virtual void LoadWorkingPoint()
    {
        if (this.workingPoint != null) return;
        this.workingPoint = transform.Find("WorkingPoint");
        Debug.Log(transform.name + " LoadObjects", gameObject);
    }

    public override void DoingTask(WorkerCtrl workerCtrl)
    {
        switch (workerCtrl.workerTasks.TaskCurrent())
        {
            case TaskType.makingResource:
                Debug.Log("makingResource ");
                break;
            default:
                if (this.IsTime2Work()) this.Planning(workerCtrl);
                break;
        }
    }

    protected virtual void Planning(WorkerCtrl workerCtrl)
    {

    }
}
