using System.Collections.Generic;
using UnityEngine;

public class ConstructionCreator : SaiBehaviour
{
    public bool isBuilding = false;
    [SerializeField] protected Vector3 buildPos;
    [SerializeField] protected ConstructionCtrl currentBuild;
    [SerializeField] protected ConstructionSpawnerCtrl ctrl;

    protected virtual void FixedUpdate()
    {
        this.ChoosePlace2Build();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadConstructionSpawnerCtrl();
    }

    protected virtual void LoadConstructionSpawnerCtrl()
    {
        if (this.ctrl != null) return;
        this.ctrl = GetComponent<ConstructionSpawnerCtrl>();
        Debug.Log(transform.name + ": ConstructionSpawnerCtrl", gameObject);
    }

    public virtual void CurrentBuildSet(string buildName)
    {
        this.isBuilding = false;
        if (this.currentBuild != null) this.currentBuild.gameObject.SetActive(false);

        foreach (ConstructionCtrl build in this.ctrl.Spawner.PoolPrefabs.Prefabs)
        {
            if (build.name != buildName) continue;
            this.currentBuild = build;
            this.currentBuild.gameObject.SetActive(true);
            Invoke(nameof(this.SetIsBuilding), 0.2f);
            return;
        }
    }

    protected virtual void SetIsBuilding()
    {
        this.isBuilding = true;
    }

    public virtual void CurrentBuildClear()
    {
        this.currentBuild.gameObject.SetActive(false);
        this.currentBuild = null;
    }

    protected virtual void ChoosePlace2Build()
    {
        if (this.currentBuild == null) return;

        Ray ray = GodModeCtrl.instance._camera.ScreenPointToRay(Input.mousePosition);

        int mask = (1 << MyLayerManager.instance.layerGround);
        if (Physics.Raycast(ray, out RaycastHit hit, 999, mask))
        {
            this.buildPos = hit.point;
            this.currentBuild.transform.position = this.buildPos;
        }
    }

    public virtual void CurrentBuildPlace()
    {
        if (this.currentBuild == null) return;

        if (this.currentBuild.limitRadius.IsCollided())
        {
            Debug.LogWarning("Collided: " + this.currentBuild.limitRadius.collideObjects.Count);
            return;
        }

        ConstructionCtrl newBuild = this.ctrl.Spawner.Spawn(this.currentBuild, this.buildPos);
        newBuild.name = this.currentBuild.name;
        newBuild.gameObject.SetActive(true);

        newBuild.abstractConstruction.isPlaced = true;
        ConstructionManager.Instance.Add(newBuild.abstractConstruction);

        this.currentBuild.gameObject.SetActive(false);
        this.currentBuild = null;
        this.isBuilding = false;
    }

    private void OnDrawGizmosSelected()
    {
        if (this.currentBuild == null) return;

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(GodModeCtrl.instance._camera.transform.position, this.buildPos);
    }
}
