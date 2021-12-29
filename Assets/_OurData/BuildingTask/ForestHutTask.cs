using System.Collections;
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
    [SerializeField] protected int storeMax = 7;
    [SerializeField] protected int storeCurrent = 0;
    [SerializeField] protected float chopSpeed = 7;

    protected override void Start()
    {
        base.Start();
        this.LoadNearByTrees();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadObjects();
        this.LoadTreePrefabs();
    }

    protected virtual void LoadObjects()
    {
        if (this.plantTreeObj != null) return;
        this.plantTreeObj = Resources.Load<GameObject>("Building/MaskPositionObject");
        Debug.Log(transform.name + " LoadObjects", gameObject);
    }

    protected virtual void LoadTreePrefabs()
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
        switch (workerCtrl.workerTasks.TaskCurrent())
        {
            case TaskType.plantTree:
                this.PlantTree(workerCtrl);
                break;
            case TaskType.findTree2Chop:
                this.FindTree2Chop(workerCtrl);
                break;
            case TaskType.chopTree:
                this.ChopTree(workerCtrl);
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
        if (this.NeedMoreTree()) workerCtrl.workerTasks.TaskAdd(TaskType.plantTree);
        if (!this.IsStoreFull())
        {
            workerCtrl.workerTasks.TaskAdd(TaskType.chopTree);
            workerCtrl.workerTasks.TaskAdd(TaskType.findTree2Chop);
        }
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
                workerCtrl.workerTasks.TaskCurrentDone();
                workerCtrl.workerTasks.TaskAdd(TaskType.goToWorkStation);
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
        TreeManager.instance.TreeAdd(treeObj);
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

    protected virtual void LoadNearByTrees()
    {
        List<GameObject> allTrees = TreeManager.instance.Trees();
        float dis;
        foreach (GameObject tree in allTrees)
        {
            dis = Vector3.Distance(tree.transform.position, transform.position);
            if (dis > this.treeRange) continue;
            this.TreeAdd(tree);
        }
    }

    public virtual void TreeAdd(GameObject tree)
    {
        if (this.trees.Contains(tree)) return;
        this.trees.Add(tree);
    }

    protected virtual void ChopTree(WorkerCtrl workerCtrl)
    {
        if (workerCtrl.workerMovement.isWorking) return;

        workerCtrl.workerMovement.isWorking = true;
        StartCoroutine(Chopping(workerCtrl, workerCtrl.workerTasks.taskTarget));
    }

    IEnumerator Chopping(WorkerCtrl workerCtrl, Transform tree)
    {
        Debug.Log("Chopping");
        yield return new WaitForSeconds(this.chopSpeed);
        Debug.Log("Chopping yield");

        TreeCtrl treeCtrl = tree.GetComponent<TreeCtrl>();
        treeCtrl.treeLevel.ShowLastBuild();
        treeCtrl.logwoodGenerator.TakeAll(ResourceName.logwood);
        treeCtrl.choper = null;
        this.trees.Remove(treeCtrl.gameObject);
        TreeManager.instance.Trees().Remove(treeCtrl.gameObject);

        workerCtrl.workerMovement.isWorking = false;
        workerCtrl.workerTasks.taskTarget = null;
        workerCtrl.workerTasks.TaskCurrentDone();
    }

    protected virtual void FindTree2Chop(WorkerCtrl workerCtrl)
    {
        WorkerTasks workerTasks = workerCtrl.workerTasks;
        if (workerTasks.inHouse) workerTasks.taskWorking.GoOutBuilding();

        if (workerCtrl.workerTasks.taskTarget == null)
        {
            this.FindNearestTree(workerCtrl);
        }
        else if (workerCtrl.workerMovement.TargetDistance() <= 1.5f)
        {
            workerCtrl.workerMovement.SetTarget(null);
            workerCtrl.workerTasks.TaskCurrentDone();
        }
    }

    protected virtual void FindNearestTree(WorkerCtrl workerCtrl)
    {
        foreach (GameObject tree in this.trees)
        {
            TreeCtrl treeCtrl = tree.GetComponent<TreeCtrl>();//TODO: can make it faster
            if (treeCtrl == null) continue;
            if (!treeCtrl.logwoodGenerator.IsAllResMax()) continue;
            if (treeCtrl.choper != null) continue;

            treeCtrl.choper = workerCtrl;
            workerCtrl.workerTasks.taskTarget = treeCtrl.transform;
            workerCtrl.workerMovement.SetTarget(treeCtrl.transform);
            return;
        }
    }

    protected virtual bool IsStoreFull()
    {
        return this.storeCurrent >= this.storeMax;
    }
}
