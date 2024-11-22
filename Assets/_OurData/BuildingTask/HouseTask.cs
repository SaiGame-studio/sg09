using System.Collections.Generic;
using UnityEngine;

public class HouseTask : BuildingTask
{
    //[Header("House")]

    public override void DoingTask(WorkerCtrl workerCtrl)
    {
        string message = workerCtrl.name + " Working at " + transform.name;
        Debug.Log(message, gameObject);
    }
}
