using UnityEngine;

public class MyLayerManager : SaiSingleton<MyLayerManager>
{
    [Header("Layers")]
    public int layerWorker;
    public int layerGround;
    public int layerBuilding;
    public int layerTree;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.GetPlayers();
    }

    protected virtual void GetPlayers()
    {
        this.layerWorker = LayerMask.NameToLayer("Worker");
        this.layerGround = LayerMask.NameToLayer("Ground");
        this.layerBuilding = LayerMask.NameToLayer("Building");
        this.layerTree = LayerMask.NameToLayer("Tree");

        if (this.layerWorker < 0) Debug.LogError("Layer Worker is mising");
        if (this.layerGround < 0) Debug.LogError("Layer Ground is mising");
        if (this.layerBuilding < 0) Debug.LogError("Layer Building is mising");
        if (this.layerTree < 0) Debug.LogError("Layer Tree is mising");

        //Debug.Log(transform.name + ": GetPlayers", gameObject);
    }
}
