using UnityEngine;

public class TextWorker : TextAbstact
{
    protected virtual void FixedUpdate()
    {
        this.ShowTestData();
    }

    protected virtual void ShowTestData()
    {
        this.textPro.text = WorkerManager.Instance.WorkingCount +" / "+ WorkerManager.Instance.Workers.Count;
    }
}
