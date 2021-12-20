using System.Collections.Generic;
using UnityEngine;

public class HouseTask : BuildingTask
{
    //[Header("House")]

    public override void DoingTask(WorkerCtrl workerCtrl)
    {
        if (!this.IsTimeToWork()) return;

        string message = workerCtrl.name + " Working at " + transform.name;
        Debug.Log(message, gameObject);
    }
}
