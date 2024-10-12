using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildButton : BtnAbstract
{
    public virtual void Build()
    {
        string buildName = transform.name.Replace("btn", "");
        ConstructionSpawnerCtrl.Instance.Creator.CurrentBuildSet(buildName);
    }

    public virtual void BuildClear()
    {
        ConstructionSpawnerCtrl.Instance.Creator.CurrentBuildClear();
    }

    protected override void OnClick()
    {
        this.Build();
    }
}
