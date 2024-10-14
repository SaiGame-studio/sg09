using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextResourceData : TextAbstact
{
    [SerializeField] protected ResourceName resourceName;

    protected virtual void FixedUpdate()
    {
        this.ShowTestData();
    }

    protected virtual void ShowTestData()
    {
        Resource resource = ResourceManager.Instance.GetResource(this.resourceName);
        this.textPro.text = resource.Number.ToString();
    }
}
