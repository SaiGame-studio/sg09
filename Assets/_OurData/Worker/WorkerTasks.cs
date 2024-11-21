using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerTasks : SaiBehaviour
{
    public WorkerCtrl workerCtrl;
    [SerializeField] protected bool isNightTime = false;//TODO: it should not be here
    [SerializeField] protected bool inHouse = false;
    public bool InHouse => inHouse;
    [SerializeField] protected bool readyForTask = false;
    public bool ReadyForTask => readyForTask;
    [SerializeField] protected TaskWorking taskWorking;
    public TaskWorking TaskWorking => taskWorking;
    [SerializeField] protected TaskGoHome taskGoHome;
    [SerializeField] protected PoolObj taskTarget;
    public PoolObj TaskTarget => taskTarget;
    public BuildingCtrl taskBuildingCtrl;
    [SerializeField] protected List<TaskType> tasks;

    protected override void Awake()
    {
        base.Awake();
        this.DisableTasks();
    }

    //protected virtual void FixedUpdate()
    //{
    //    this.Working();
    //}

    public virtual void Working()
    {
        if (this.isNightTime) this.WorkAtHome();
        else this.WorkAtStation();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadWorkerCtrl();
        this.LoadTasks();
    }

    protected virtual void LoadWorkerCtrl()
    {
        if (this.workerCtrl != null) return;
        this.workerCtrl = GetComponent<WorkerCtrl>();
        Debug.Log(transform.name + ": LoadWorkerCtrl", gameObject);
    }

    protected virtual void LoadTasks()
    {
        if (this.taskWorking != null) return;
        Transform tasksObj = transform.Find("Tasks");
        this.taskWorking = tasksObj.GetComponentInChildren<TaskWorking>();
        this.taskGoHome = tasksObj.GetComponentInChildren<TaskGoHome>();
        Debug.Log(transform.name + ": LoadTasks", gameObject);
    }

    protected virtual void DisableTasks()
    {
        this.taskWorking.gameObject.SetActive(false);
        this.taskGoHome.gameObject.SetActive(false);
    }

    protected virtual void WorkAtHome()
    {
        this.taskGoHome.Working();
    }

    protected virtual void WorkAtStation()
    {
        this.taskWorking.Working();
    }

    public virtual void TaskAdd(TaskType taskType)
    {
        TaskType currentTask = this.TaskCurrent();
        if (taskType == currentTask) return;
        this.tasks.Add(taskType);
    }

    public virtual void TaskCurrentDone()
    {
        if (this.tasks.Count <= 0) return;
        this.tasks.RemoveAt(this.tasks.Count - 1);
        this.workerCtrl.workerMovement.SetTarget(null);
    }

    public virtual void ClearAllTasks()
    {
        this.tasks.Clear();
    }

    public virtual TaskType TaskCurrent()
    {
        if (this.tasks.Count <= 0) return TaskType.none;
        return this.tasks[this.tasks.Count - 1];
    }

    public virtual void SetTaskTarget(PoolObj target)
    {
        this.taskTarget = target;
    }

    public virtual void SetReadyForTask(bool status)
    {
        this.readyForTask = status;
    }

    public virtual void SetInHouse(bool status)
    {
        this.inHouse = status;
    }
}
