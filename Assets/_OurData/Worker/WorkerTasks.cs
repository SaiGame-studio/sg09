using UnityEngine;

public class WorkerTasks : SaiBehaviour
{
    public WorkerCtrl workerCtrl;
    [SerializeField] protected bool isNightTime = false;
    [SerializeField] protected WorkerTask taskWorking;
    [SerializeField] protected WorkerTask taskGoHome;

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
    }

    protected virtual void LoadWorkerCtrl()
    {
        if (this.workerCtrl != null) return;
        this.workerCtrl = GetComponent<WorkerCtrl>();
        Debug.Log(transform.name + ": LoadWorkerCtrl", gameObject);
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
