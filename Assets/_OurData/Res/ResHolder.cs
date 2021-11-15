using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResHolder : MonoBehaviour
{
    [SerializeField] protected ResourceName resourceName;
    [SerializeField] protected float resCurrent = 0;
    [SerializeField] protected float resMax = Mathf.Infinity;

    private void Awake()
    {
        this.LoadResName();
    }

    protected virtual void LoadResName()
    {
        string name = transform.name;
        this.resourceName = ResNameParser.FromString(name);
    }
}
