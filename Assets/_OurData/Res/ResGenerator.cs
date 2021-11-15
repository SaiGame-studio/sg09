using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResGenerator : MonoBehaviour
{
    [SerializeField] protected List<ResHolder> resHolders;

    protected void Awake()
    {
        this.LoadHolders();
    }

    protected virtual void LoadHolders()
    {
        Transform res = transform.Find("Res");
        foreach (Transform resTran in res)
        {
            Debug.Log(resTran.name);
        }
    }
}
