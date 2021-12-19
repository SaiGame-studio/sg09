using UnityEngine;

public class ForestHutTask : BuildingTask
{
    public override void DoingTask()
    {
        Debug.Log(transform.name + " DoingTask", gameObject);
    }
}
