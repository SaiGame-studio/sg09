using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildWorker : AbstractConstruction
{
    //[Header("Worker")]

    protected override void LoadBuildNames()
    {
        if (this.buildNames.Count > 0) return;
        this.buildNames.Add("Kachujin");
        this.buildNames.Add("ErikaArcher");
        this.buildNames.Add("AkaiEspiritu");
        this.buildNames.Add("Arissa");
        Debug.Log(transform.name + ": LoadBuildNames", gameObject);
    }

    protected override Transform FinishBuild()
    {
        Transform newBuild = base.FinishBuild();
        newBuild.parent = WorkerManager.instance.transform;
        return newBuild;
    }
}
