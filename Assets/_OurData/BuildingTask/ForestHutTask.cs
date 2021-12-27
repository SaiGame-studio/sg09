using System.Collections.Generic;
using UnityEngine;

public class ForestHutTask : BuildingTask
{
    [Header("Forest Hut")]
    [SerializeField] protected GameObject plantTreeObj;
    [SerializeField] protected List<GameObject> treePrefabs;
    [SerializeField] protected List<GameObject> trees;
    [SerializeField] protected int treeMax = 7;
    [SerializeField] protected float treeRange = 27f;
    [SerializeField] protected float treeDistance = 7f;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadObjects();
        this.LoadTrees();
    }

    protected virtual void LoadObjects()
    {
        if (this.plantTreeObj != null) return;
        this.plantTreeObj = Resources.Load<GameObject>("Building/MaskPositionObject");
        Debug.Log(transform.name + " LoadObjects", gameObject);
    }

    protected virtual void LoadTrees()
    {
        if (this.treePrefabs.Count > 0) return;
        GameObject tree1 = Resources.Load<GameObject>("Res/Tree_1");
        GameObject tree2 = Resources.Load<GameObject>("Res/Tree_2");
        GameObject tree3 = Resources.Load<GameObject>("Res/Tree_3");
        this.treePrefabs.Add(tree1);
        this.treePrefabs.Add(tree2);
        this.treePrefabs.Add(tree3);
        Debug.Log(transform.name + " LoadObjects", gameObject);
    }

    public override void DoingTask(WorkerCtrl workerCtrl)
    {
        WorkerTask taskWorking = workerCtrl.workerTasks.taskWorking;
        TaskType currentTask = taskWorking.TaskCurrent();
        switch (currentTask)
        {
            case TaskType.plantTree:
                this.PlantTree(workerCtrl);
                break;
            case TaskType.goToWorkStation:
                this.BackToWorkStation(workerCtrl);
                break;
            default:
                if (this.IsTime2Work()) this.Planning(workerCtrl);
                break;
        }
    }

    protected virtual void Planning(WorkerCtrl workerCtrl)
    {
        WorkerTask taskWorking = workerCtrl.workerTasks.taskWorking;
        if (this.NeedMoreTree()) taskWorking.TaskAdd(TaskType.plantTree);
    }

    protected virtual bool NeedMoreTree()
    {
        return this.treeMax >= this.trees.Count;
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
            workerCtrl.workerMovement.SetTarget(null);
            Destroy(target.gameObject);//TODO: not done yet
            this.Planting(workerCtrl.transform);

            if (!this.NeedMoreTree())
            {
                workerCtrl.workerTasks.taskWorking.TaskCurrentDone();
                workerCtrl.workerTasks.taskWorking.TaskAdd(TaskType.goToWorkStation);
            }
        }
    }

    protected virtual void Planting(Transform trans)
    {
        GameObject treePrefab = this.GetTreePrefab();
        GameObject treeObj = Instantiate<GameObject>(treePrefab);
        treeObj.transform.position = trans.position;
        treeObj.transform.rotation = trans.rotation;
        this.trees.Add(treeObj);

    }

    protected virtual GameObject GetTreePrefab()
    {
        int rand = Random.Range(0, this.treePrefabs.Count);
        return this.treePrefabs[rand];
    }

    protected virtual Transform GetPlantPlace()
    {
        Vector3 newTreePos = this.RandomPlaceForTree(); ;
        float dis = Vector3.Distance(transform.position, newTreePos);
        if (dis < this.treeDistance) return null;

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
