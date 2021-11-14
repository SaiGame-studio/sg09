using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceGenerator : MonoBehaviour
{
    [Header("Generator")]
    [SerializeField] protected ResourceName resourceName;
    [SerializeField] protected int number = 1;
    [SerializeField] protected float speed = 2;
    [SerializeField] protected float timer = Mathf.Infinity;

    protected virtual void FixedUpdate()
    {
        this.Generating();
    }

    protected virtual void Generating()
    {
        if (this.resourceName == 0) return;

        this.timer += Time.fixedDeltaTime;
        if (this.timer < this.speed) return;
        this.timer = 0;

        ResourceManager.instance.AddByName(this.resourceName, this.number);
    }
}
