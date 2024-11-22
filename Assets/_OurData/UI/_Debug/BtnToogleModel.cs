using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnToogleModel : BtnAbstract
{
    protected override void OnClick()
    {
        WorkerManager.Instance.ToogleModel();
    }
}
