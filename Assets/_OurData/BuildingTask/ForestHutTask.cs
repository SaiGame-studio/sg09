using UnityEngine;

public class ForestHutTask : BuildingTask
{
    [Header("Forest Hut")]
    [SerializeField] protected GameObject plantTreeObj;
    [SerializeField] protected GameObject treePrefab;
    [SerializeField] protected float treeRange = 27f;
    [SerializeField] protected float treeDistance = 7f;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadObjects();
    }

    protected virtual void LoadObjects()
    {
        if (this.plantTreeObj != null) return;
        this.plantTreeObj = Resources.Load<GameObject>("Building/MaskPositionObject");
        this.treePrefab = Resources.Load<GameObject>("Res/Tree");
        Debug.Log(transform.name + " LoadObjects", gameObject);
    }

    public override void DoingTask(WorkerCtrl workerCtrl)
    {
        if (!this.IsTime2Work()) return;

        string message = workerCtrl.name + " Working at " + transform.name;
        Debug.Log(message, gameObject);

        this.PlantTree(workerCtrl);
    }

    protected virtual void PlantTree(WorkerCtrl workerCtrl)
    {
        Transform target = workerCtrl.workerMovement.GetTarget();

        if (target == null) target = this.GetPlantPlace();
        if (target == null) return;

        workerCtrl.workerTasks.taskWorking.GoOutBuilding();
        workerCtrl.workerMovement.SetTarget(target);

        if (workerCtrl.workerMovement.IsClose2Target())
        {
            this.Planting(workerCtrl.transform);
            workerCtrl.workerMovement.SetTarget(null);
            Destroy(target.gameObject);
        }
    }

    protected virtual void Planting(Transform trans)
    {
        GameObject treeObj = Instantiate<GameObject>(this.treePrefab);
        treeObj.transform.position = trans.position;
        treeObj.transform.rotation = trans.rotation;
    }

    protected virtual Transform GetPlantPlace()
    {
        Vector3 newTreePos = this.RandomPlaceForTree(); ;
        float dis = Vector3.Distance(transform.position, newTreePos);
        if (dis < this.treeDistance)
        {
            Debug.Log("GetPlantPlace Destroy GameObject");
            return null;
        }

        GameObject treePlace = Instantiate(this.plantTreeObj);
        treePlace.transform.position = newTreePos;

        return treePlace.transform;
    }

    protected virtual Vector3 RandomPlaceForTree()
    {
        Vector3 position = transform.position;
        position.x += Random.Range(this.treeRange * -1, this.treeRange);
        position.y = 0;
        position.z += Random.Range(this.treeRange * -1, this.treeRange);

        return position;
    }
}
