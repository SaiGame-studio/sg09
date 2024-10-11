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
    [SerializeField] protected List<TreeCtrl> trees;

    protected override void Start()
    {
        base.Start();
        InvokeRepeating(nameof(this.LoadNearByTrees), 7f, 7f);
    }

    protected virtual void FixedUpdate()
    {
        this.RemoveDeadTrees();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadObjects();
    }

    protected virtual void LoadObjects()
    {
        if (this.plantTreeObj != null) return;
        this.plantTreeObj = Resources.Load<GameObject>("Building/MaskPositionObject");
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
        if (this.NeedMoreTree())
        {
            workerCtrl.workerMovement.SetTarget(null);
            workerCtrl.workerTasks.TaskAdd(TaskType.plantTree);
            return;
        }

        if (!this.buildingCtrl.warehouse.IsFull())
        {
            workerCtrl.workerTasks.TaskAdd(TaskType.bringResourceBack);
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
            this.Planting(workerCtrl);
            workerCtrl.workerMovement.SetTarget(null);

            if (!this.NeedMoreTree())
            {
                workerCtrl.workerTasks.TaskCurrentDone();
                workerCtrl.workerTasks.TaskAdd(TaskType.goToWorkStation);
            }
        }
    }

    protected virtual void Planting(WorkerCtrl workerCtrl)
    {
        TreeCtrl treePrefab = this.GetTreePrefab();
        Vector3 plantPos = workerCtrl.transform.position;
        plantPos.y -= 0.1f;
        TreeCtrl treeObj = TreeSpawnerCtrl.Instance.Spawner.Spawn(treePrefab, plantPos);
        treeObj.transform.rotation = workerCtrl.transform.rotation;
        treeObj.gameObject.SetActive(true);
        this.trees.Add(treeObj);
    }

    protected virtual TreeCtrl GetTreePrefab()
    {
        List<TreeCtrl> treePrefab = TreeSpawnerCtrl.Instance.Spawner.PoolPrefabs.Prefabs;
        int rand = Random.Range(0, treePrefab.Count);
        return treePrefab[rand];
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
        float dis;
        foreach (TreeCtrl tree in TreeSpawnerCtrl.Instance.Manager.Trees)
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
        StartCoroutine(Chopping(workerCtrl, (TreeCtrl)workerCtrl.workerTasks.TaskTarget));
    }

    private IEnumerator Chopping(WorkerCtrl workerCtrl, TreeCtrl treeCtrl)
    {
        workerCtrl.workerMovement.isWorking = true;
        yield return new WaitForSeconds(this.workingSpeed);

        treeCtrl.treeLevel.ShowLastBuild();
        List<Resource> resources = treeCtrl.logwoodGenerator.TakeAll();
        treeCtrl.choper = null;
        this.trees.Remove(treeCtrl);
        TreeSpawnerCtrl.Instance.Manager.Remove(treeCtrl);

        workerCtrl.workerMovement.isWorking = false;
        workerCtrl.workerTasks.SetTaskTarget(null);
        workerCtrl.resCarrier.AddByList(resources);

        workerCtrl.workerTasks.TaskCurrentDone();

        StartCoroutine(this.DespawnTree(treeCtrl));
    }

    private IEnumerator DespawnTree(TreeCtrl treeCtrl)
    {
        yield return new WaitForSeconds(this.treeRemoveSpeed);
        treeCtrl.Despawn.DoDespawn();
    }

    protected virtual void FindTree2Chop(WorkerCtrl workerCtrl)
    {
        WorkerTasks workerTasks = workerCtrl.workerTasks;
        if (workerTasks.inHouse) workerTasks.taskWorking.GoOutBuilding();

        if (workerCtrl.workerTasks.TaskTarget == null)
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
            Debug.Log("FindNearestTree: " + tree.name, tree.gameObject);

            if (tree == null) continue;
            if (!tree.logwoodGenerator.IsAllResMax()) continue;
            if (tree.choper != null) continue;

            tree.choper = workerCtrl;
            workerCtrl.workerTasks.SetTaskTarget(tree);
            workerCtrl.workerMovement.SetTarget(tree.transform);
            Debug.Log("FOUND: " + tree.name, tree.gameObject);
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
