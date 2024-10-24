using UnityEngine;

public class TextTree : TextAbstact
{
    protected virtual void FixedUpdate()
    {
        this.ShowTestData();
    }

    protected virtual void ShowTestData()
    {
        this.textPro.text = TreeManager.Instance.Trees.Count.ToString();
    }
}
