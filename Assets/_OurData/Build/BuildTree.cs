using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildTree : BuildBuilding
{
    //[Header("Tree")]

    protected override void LoadBuildNames()
    {
        if (this.buildNames.Count > 0) return;
        this.buildNames.Add("Tree_1");
        this.buildNames.Add("Tree_2");
        this.buildNames.Add("Tree_3");
        Debug.Log(transform.name + ": LoadBuildNames", gameObject);
    }

    protected override Transform FinishBuild()
    {
        Transform newBuild = base.FinishBuild();
        TreeManager.instance.TreeAdd(newBuild.gameObject);
        return newBuild;
    }
}
