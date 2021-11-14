using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceGenerator : MonoBehaviour
{
    [SerializeField] protected ResourceName resourceName;
    [SerializeField] protected float speed = 2f;
    [SerializeField] protected float timer = 0;
    [SerializeField] protected int number = 1;

    protected void FixedUpdate()
    {
        this.Generating();
    }

    protected virtual void Generating()
    {
        if (this.resourceName == ResourceName.noResource) return;

        this.timer += Time.fixedDeltaTime;
        if (this.timer < this.speed) return;
        this.timer = 0;

        ResourceManager.instance.AddResource(this.resourceName, this.number);
    }
}
