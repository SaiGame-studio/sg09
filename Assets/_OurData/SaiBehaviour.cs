using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaiBehaviour : MonoBehaviour
{
    protected virtual void Awake()
    {
        this.LoadComponents();
    }

    protected virtual void Reset()
    {
        this.LoadComponents();
    }

    protected virtual void FixedUpdate()
    {
        //For Overide
    }

    protected virtual void OnDisable()
    {
        //For Overide
    }

    protected virtual void OnEnable()
    {
        //For Overide
    }

    protected virtual void LoadComponents()
    {
        //For Overide
    }
}
