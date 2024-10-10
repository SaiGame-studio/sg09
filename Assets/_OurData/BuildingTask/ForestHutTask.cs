using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestHutTask : BuildingTask
{
    [Header("Forest Hut")]
    [SerializeField] protected GameObject plantTreeObj;
    [SerializeField] protected int treeMax = 7;
    [SerializeField] protected float treeRange = 27f;
    [SerializeField] protected float treeDistance = 7f;
    [SerializeField] protected float treeRemoveSpeed = 16;
    [SerializeField] protected List<GameObject> treePrefabs;
    [SerializeField] protected List<TreeCtrl> trees;

    protected override void Start()
    {
        base.Start();
        this.LoadNearByTrees();
    }

    protected virtual void FixedUpdate()
    {
        this.RemoveDeadTrees();
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

    protected virtual void RemoveDeadTrees()
    {
        TreeCtrl tree;
        for (int i = 0; i < this.trees.Count; i++)
        {
            tree = this.trees[i];
            if (tree == null) this.trees.RemoveAt(i);
        }
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
            case TaskType.bringResourceBack:
                this.BringTreeBack(workerCtrl);
                break;
            case TaskType.goToWorkStation:
                this.GoToWorkStation(workerCtrl);
                break;
            default:
                if (this.IsTime2Work()) this.Planning(workerCtrl);
                break;
        }
    }

    protected virtual void Planning(WorkerCtrl workerCtrl)
    {

        if (!this.buildingCtrl.warehouse.IsFull())
        {
            workerCtrl.workerTasks.TaskAdd(TaskType.bringResourceBack);
            workerCtrl.workerTasks.TaskAdd(TaskType.chopTree);
            workerCtrl.workerTasks.TaskAdd(TaskType.findTree2Chop);
        }

        if (this.NeedMoreTree())
        {
            workerCtrl.workerMovement.SetTarget(null);
            workerCtrl.workerTasks.TaskAdd(TaskType.plantTree);
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
            PrefabManager.instance.Destroy(target);
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
        TreeCtrl treePrefab = this.GetTreePrefab();
        TreeCtrl treeObj = TreeSpawnerCtrl.Instance.Spawner.Spawn(treePrefab, trans.position);
        treeObj.transform.rotation = trans.rotation;
        this.trees.Add(treeObj);
    }

    protected virtual TreeCtrl GetTreePrefab()
    {
        int rand = Random.Range(0, this.treePrefabs.Count);
        return TreeSpawnerCtrl.Instance.Spawner.PoolObjects[rand];
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
        List<TreeCtrl> allTrees = TreeSpawnerCtrl.Instance.Spawner.PoolObjects;
        float dis;
        foreach (TreeCtrl tree in allTrees)
        {
            dis = Vector3.Distance(tree.transform.position, transform.position);
            if (dis > this.treeRange) continue;
            this.TreeAdd(tree);
        }
    }

    public virtual void TreeAdd(TreeCtrl tree)
    {
        if (this.trees.Contains(tree)) return;
        this.trees.Add(tree);
    }

    protected virtual void ChopTree(WorkerCtrl workerCtrl)
    {
        if (workerCtrl.workerMovement.isWorking) return;
        StartCoroutine(Chopping(workerCtrl, workerCtrl.workerTasks.taskTarget));
    }

    private IEnumerator Chopping(WorkerCtrl workerCtrl, Transform tree)
    {
        workerCtrl.workerMovement.isWorking = true;
        yield return new WaitForSeconds(this.workingSpeed);

        TreeCtrl treeCtrl = tree.GetComponent<TreeCtrl>();
        treeCtrl.treeLevel.ShowLastBuild();
        List<Resource> resources = treeCtrl.logwoodGenerator.TakeAll();
        treeCtrl.choper = null;
        this.trees.Remove(treeCtrl);
        treeCtrl.Despawn.DoDespawn();

        workerCtrl.workerMovement.isWorking = false;
        workerCtrl.workerTasks.taskTarget = null;
        workerCtrl.resCarrier.AddByList(resources);

        workerCtrl.workerTasks.TaskCurrentDone();

        StartCoroutine(this.RemoveTree(tree));
    }

    private IEnumerator RemoveTree(Transform tree)
    {
        yield return new WaitForSeconds(this.treeRemoveSpeed);
        PrefabManager.instance.Destroy(tree);
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
            //workerCtrl.workerMovement.SetTarget(null);
            workerCtrl.workerTasks.TaskCurrentDone();
        }
    }

    protected virtual void FindNearestTree(WorkerCtrl workerCtrl)
    {
        foreach (TreeCtrl tree in this.trees)
        {
            if (tree == null) continue;
            if (!tree.logwoodGenerator.IsAllResMax()) continue;
            if (tree.choper != null) continue;

            tree.choper = workerCtrl;
            workerCtrl.workerTasks.taskTarget = tree.transform;
            workerCtrl.workerMovement.SetTarget(tree.transform);
            return;
        }
    }

    protected virtual void BringTreeBack(WorkerCtrl workerCtrl)
    {
        WorkerTask taskWorking = workerCtrl.workerTasks.taskWorking;
        taskWorking.GotoBuilding();
        if (!workerCtrl.workerMovement.IsClose2Target()) return;

        List<Resource> resources = workerCtrl.resCarrier.TakeAll();
        this.buildingCtrl.warehouse.AddByList(resources);
        taskWorking.GoIntoBuilding();

        workerCtrl.workerTasks.TaskCurrentDone();
    }
}
