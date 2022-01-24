using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildButton : MonoBehaviour
{
    public virtual void Build()
    {
        string buildName = transform.name.Replace("btn", "");
        BuildManager.instance.CurrentBuildSet(buildName);
    }

    public virtual void BuildClear()
    {
        BuildManager.instance.CurrentBuildClear();
    }
}
