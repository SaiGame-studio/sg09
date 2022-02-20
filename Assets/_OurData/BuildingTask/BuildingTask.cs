using System.Collections.Generic;
using UnityEngine;

public class BuildingTask : SaiBehaviour
{
    [Header("Building Task")]
    public BuildingCtrl buildingCtrl;
    [SerializeField] protected float taskTimer = 0;
    [SerializeField] protected float taskDelay = 5f;
    [SerializeField] protected float workingSpeed = 7;
    [SerializeField] protected int lastBuildingWorked = 0;
    [SerializeField] protected List<BuildingCtrl> nearBuildings;

    protected override void Start()
    {
        base.Start();
        this.FindNearBuildings();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadBuildingCtrl();
    }

    protected virtual void LoadBuildingCtrl()
    {
        if (this.buildingCtrl != null) return;
        this.buildingCtrl = GetComponent<BuildingCtrl>();
        Debug.Log(transform.name + " LoadBuildingCtrl", gameObject);
    }

    protected virtual bool IsTime2Work()
    {
        this.taskTimer += Time.fixedDeltaTime;
        if (this.taskTimer < this.taskDelay) return false;
        this.taskTimer = 0;
        return true;
    }

    protected virtual void GoToWorkStation(WorkerCtrl workerCtrl)
    {
        WorkerTask taskWorking = workerCtrl.workerTasks.taskWorking;
        taskWorking.GotoBuilding();
        if (workerCtrl.workerMovement.IsClose2Target())
        {
            taskWorking.GoIntoBuilding();
            workerCtrl.workerTasks.TaskCurrentDone();
        }
    }

    public virtual void DoingTask(WorkerCtrl workerCtrl)
    {
        //For override
    }

    public virtual void FindNearBuildings()
    {
        this.nearBuildings.Clear();
        this.nearBuildings = new List<BuildingCtrl>(BuildingManager.instance.BuildingCtrls());
        this.nearBuildings.Sort(delegate (BuildingCtrl a, BuildingCtrl b)
        {
            Vector3 aPos = a.transform.position;
            Vector3 bPos = b.transform.position;
            Vector3 currentPos = transform.position;
            return Vector3.Distance(currentPos, aPos)
            .CompareTo(Vector3.Distance(currentPos, bPos));
        });
        //Invoke("FindNearBuildings", 7f);
    }
}