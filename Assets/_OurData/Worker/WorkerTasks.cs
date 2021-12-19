using UnityEngine;

public class WorkerTasks : SaiBehaviour
{
    public WorkerCtrl workerCtrl;
    [SerializeField] protected bool isNightTime = false;
    [SerializeField] protected WorkerTask taskWorking;
    [SerializeField] protected WorkerTask taskGoHome;

    protected override void Awake()
    {
        base.Awake();
        this.DisableTasks();
        //InvokeRepeating("Testing", 2f,5f);
    }

    protected virtual void Testing()
    {
        this.isNightTime = !this.isNightTime;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        if (this.isNightTime) this.GoHome();
        else this.GoWork();
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

    protected virtual void GoHome()
    {
        this.taskWorking.gameObject.SetActive(false);
        this.taskGoHome.gameObject.SetActive(true);
    }

    protected virtual void GoWork()
    {
        this.taskWorking.gameObject.SetActive(true);
        this.taskGoHome.gameObject.SetActive(false);
    }
}
