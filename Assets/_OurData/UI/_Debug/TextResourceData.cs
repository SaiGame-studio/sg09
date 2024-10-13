using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextResourceData : TextAbstact
{
    [SerializeField] protected ResHolder resHolder;

    protected virtual void FixedUpdate()
    {
        this.ShowTestData();
    }

    protected virtual void ShowTestData()
    {
        this.textPro.text = resHolder.resCurrent.ToString();
    }
}
