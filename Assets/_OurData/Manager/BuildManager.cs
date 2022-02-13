using System.Collections.Generic;
using UnityEngine;

public class BuildManager : SaiBehaviour
{
    public static BuildManager instance;
    public bool isBuilding = false;
    [SerializeField] protected Vector3 buildPos;
    [SerializeField] protected Transform currentBuild;
    [SerializeField] protected List<Transform> buildPrefabs;

    protected override void Awake()
    {
        base.Awake();
        if (BuildManager.instance != null) Debug.LogError("Only 1 BuildManager allow");
        BuildManager.instance = this;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        this.ChoosePlace2Build();
    }

    protected override void Start()
    {
        base.Start();
        this.HideAllPrefabs();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadBuildPrefabs();
    }

    protected virtual void LoadBuildPrefabs()
    {
        if (this.buildPrefabs.Count > 0) return;
        foreach (Transform child in transform)
        {
            this.buildPrefabs.Add(child);
        }

        Debug.Log(transform.name + ": LoadBuildPrefabs", gameObject);
    }

    protected virtual void HideAllPrefabs()
    {
        foreach (Transform build in this.buildPrefabs)
        {
            build.gameObject.SetActive(false);
        }
    }

    public virtual void CurrentBuildSet(string buildName)
    {
        this.isBuilding = false;
        if (this.currentBuild != null) this.currentBuild.gameObject.SetActive(false);

        //Debug.Log(transform.name + ": " + buildName);
        foreach (Transform build in this.buildPrefabs)
        {
            //Debug.Log("build.name: " + build.name);
            if (build.name != buildName) continue;

            //Debug.Log("Found");
            this.currentBuild = build;
            this.currentBuild.gameObject.SetActive(true);
            Invoke("SetIsBuilding", 0.2f);
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
            this.currentBuild.position = this.buildPos;
        }
    }

    public virtual void CurrentBuildPlace()
    {
        if (this.currentBuild == null) return;

        ConstructionCtrl constructionCtrl = this.currentBuild.GetComponent<ConstructionCtrl>();
        if (constructionCtrl && constructionCtrl.limitRadius.IsCollided())
        {
            Debug.LogWarning("Collided: " + constructionCtrl.limitRadius.collideObjects.Count);
            return;
        }

        string constructioName = constructionCtrl.abstractConstruction.GetConstructionName();
        if (constructioName != null)
        {
            Transform newBuild = PrefabManager.instance.Instantiate(constructioName);
            newBuild.position = this.buildPos;
            newBuild.name = this.currentBuild.name;
            newBuild.gameObject.SetActive(true);

            AbstractConstruction abstractConstruction = newBuild.GetComponent<AbstractConstruction>();
            abstractConstruction.isPlaced = true;
            ConstructionManager.instance.AddConstruction(abstractConstruction);
        }

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
