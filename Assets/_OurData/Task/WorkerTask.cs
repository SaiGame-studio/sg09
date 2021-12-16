using UnityEngine;

public class WorkerTask : SaiBehaviour
{
    public WorkerCtrl workerCtrl;
    [SerializeField] protected bool inHouse = false;
    [SerializeField] protected float buildingDistance = 0;
    [SerializeField] protected float buildDisLimit = 0.7f;

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        if (this.GetBuilding()) this.WorkPlanning();
        else this.FindBuilding();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadWorkerCtrl();
    }

    protected virtual void LoadWorkerCtrl()
    {
        if (this.workerCtrl != null) return;
        this.workerCtrl = transform.parent.parent.GetComponent<WorkerCtrl>();
        Debug.Log(transform.name + ": LoadWorkerCtrl", gameObject);
    }

    protected virtual void FindBuilding()
    {
        BuildingCtrl buildingCtrl = BuildingManager.instance.FindBuilding(transform, this.GetBuildingType());
        if (buildingCtrl == null) return;
        this.AssignBuilding(buildingCtrl);
    }

    protected virtual void GotoBuilding()
    {
        BuildingCtrl buildingCtrl = this.GetBuilding();
        this.workerCtrl.workerMovement.SetTarget(buildingCtrl.door);
    }

    public virtual bool IsAtBuilding()
    {
        return this.BuildingDistance() < this.buildDisLimit;
    }

    protected virtual float BuildingDistance()
    {
        BuildingCtrl buildingCtrl = this.GetBuilding();
        this.buildingDistance = Vector3.Distance(transform.position, buildingCtrl.door.transform.position);
        return this.buildingDistance;
    }

    protected virtual void WorkPlanning()
    {
        if (this.IsAtBuilding()) this.GoIntoBuilding();
        else this.GotoBuilding();

        if(this.inHouse) this.Working(); ;
    }

    protected virtual void GoIntoBuilding()
    {
        this.workerCtrl.workerMovement.SetTarget(null);
        this.inHouse = true;
        //this.workerCtrl.workerModel.gameObject.SetActive(false);
    }

    protected virtual void Working()
    {
        //For overide
    }

    protected virtual BuildingCtrl GetBuilding()
    {
        //For overide
        return null;
    }

    protected virtual void AssignBuilding(BuildingCtrl buildingCtrl)
    {
        //For overide
    }

    protected virtual BuildingType GetBuildingType()
    {
        return BuildingType.work_station;
    }
}
