using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildTree : AbstractConstruction
{
    //[Header("Tree")]

    protected override void LoadBuildNames()
    {
        if (this.buildNames.Count > 0) return;
        this.buildNames.Add("Tree_1");
        Debug.Log(transform.name + ": LoadBuildNames", gameObject);
    }

    protected override Transform FinishBuild()
    {
        Transform newBuild = base.FinishBuild();
        TreeManager.instance.TreeAdd(newBuild.gameObject);
        return newBuild;
    }
}
