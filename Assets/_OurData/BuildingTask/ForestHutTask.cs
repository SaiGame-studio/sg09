using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestHutTask : BuildingTask
{
    [Header("Forest Hut")]
    [SerializeField] protected int treeMax = 700;
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

    protected virtual void RemoveDeadTrees()
    {
        TreeCtrl tree;
        for (int i = 0; i < this.trees.Count; i++)
        {
            tree = this.trees[i];
            if (tree == null || !tree.gameObject.activeSelf) this.trees.RemoveAt(i);
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
        if (this.HasTreeFullLevel() && !this.ctrl.warehouse.IsFull())
        {
            workerCtrl.workerTasks.TaskAdd(TaskType.bringResourceBack);
            workerCtrl.workerTasks.TaskAdd(TaskType.chopTree);
            workerCtrl.workerTasks.TaskAdd(TaskType.findTree2Chop);
        }
        else if (this.NeedMoreTree())
        {
            workerCtrl.workerMovement.SetTarget(null);
            workerCtrl.workerTasks.TaskAdd(TaskType.plantTree);
        }
        else
        {
            if(!workerCtrl.workerTasks.InHouse) workerCtrl.workerTasks.TaskAdd(TaskType.goToWorkStation);
        }
    }

    protected virtual bool HasTreeFullLevel()
    {
        foreach (TreeCtrl tree in this.trees)
        {
            if (tree == null) continue;
            if (!tree.LogwoodGenerator.IsAllResMax()) continue;
            if (tree.choper != null) continue;
            return true;
        }

        return false;
    }

    protected virtual void FindNearestTree(WorkerCtrl workerCtrl)
    {

        foreach (TreeCtrl tree in this.trees)
        {
            if (tree == null) continue;
            if (!tree.LogwoodGenerator.IsAllResMax()) continue;
            if (tree.choper != null) continue;

            tree.choper = workerCtrl;
            workerCtrl.workerTasks.SetTaskTarget(tree);
            workerCtrl.workerMovement.SetTarget(tree.transform);
            return;
        }
    }

    protected virtual bool NeedMoreTree()
    {
        return this.treeMax >= this.trees.Count;
    }

    protected virtual void PlantTree(WorkerCtrl workerCtrl)
    {
        Transform target = workerCtrl.workerMovement.GetTarget();

        if (target == null)
        {
            TreePlantPositionCtrl treePlant = this.GetPlantPosition();
            if (treePlant == null) return;

            if (!workerCtrl.workerMovement.CanReachDestination(treePlant.transform.position))
            {
                //Debug.LogError("Cant go to " + treePlant.transform.position, treePlant.gameObject);
                treePlant.Despawn.DoDespawn();
                return;
            }

            target = treePlant.transform;
        }

        workerCtrl.workerTasks.TaskWorking.GoOutBuilding();
        workerCtrl.workerMovement.SetTarget(target);

        if (workerCtrl.workerMovement.IsCloseToTarget())
        {
            this.Planting(workerCtrl);
            workerCtrl.workerMovement.SetTarget(null);
            workerCtrl.workerTasks.TaskCurrentDone();
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

    protected virtual TreePlantPositionCtrl GetPlantPosition()
    {
        Vector3 newTreePos = this.RandomPlaceForTree();
        float dis = Vector3.Distance(transform.position, newTreePos);
        if (dis < this.treeDistance) return null;

        EffectCtrl newObj = EffectSpawnerCtrl.Instance.Spawner.Spawn(EffectName.TreePlantPosition, newTreePos);
        newObj.gameObject.SetActive(true);

        return (TreePlantPositionCtrl)newObj;
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

        treeCtrl.TreeLevel.ShowLastBuild();
        List<Resource> resources = treeCtrl.LogwoodGenerator.TakeAll();
        treeCtrl.choper = null;
        this.trees.Remove(treeCtrl);
        TreeSpawnerCtrl.Instance.Manager.Remove(treeCtrl);

        workerCtrl.workerMovement.isWorking = false;
        workerCtrl.workerTasks.SetTaskTarget(null);
        workerCtrl.inventory.AddResources(resources);

        workerCtrl.buildings.WorkBuilding.warehouse.WillAdd(resources);

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
        if (workerTasks.InHouse) workerTasks.TaskWorking.GoOutBuilding();

        if (workerCtrl.workerTasks.TaskTarget == null)
        {
            this.FindNearestTree(workerCtrl);
        }
        else if (workerCtrl.workerMovement.TargetDistance() <= 1.5f)
        {
            workerCtrl.workerTasks.TaskCurrentDone();
        }
    }

    protected virtual void BringTreeBack(WorkerCtrl workerCtrl)
    {
        WorkerTask taskWorking = workerCtrl.workerTasks.TaskWorking;
        taskWorking.GotoBuilding();
        if (!workerCtrl.workerMovement.IsCloseToTarget()) return;

        List<Resource> resources = workerCtrl.inventory.TakeAll();
        this.ctrl.warehouse.AddResources(resources);
        this.ctrl.warehouse.Added(resources);
        taskWorking.GoIntoBuilding();

        workerCtrl.workerTasks.TaskCurrentDone();
    }
}
