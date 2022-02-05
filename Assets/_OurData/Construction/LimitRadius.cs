using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class LimitRadius : SaiBehaviour
{
    [Header("Limit Radius")]
    [SerializeField] protected float buildRadius = 7f;
    [SerializeField] protected SphereCollider _collider;
    public List<GameObject> collideObjects;


    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCollider();
    }

    protected virtual void LoadCollider()
    {
        if (this._collider != null) return;
        this._collider = GetComponent<SphereCollider>();
        this._collider.radius = this.buildRadius;
        this._collider.isTrigger = true;
        Debug.Log(transform.name + ": LoadCollider", gameObject);
    }

    public virtual bool IsCollided()
    {
        return this.collideObjects.Count > 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        bool canCollide = false;
        int coliderLayer = other.gameObject.layer;

        if (coliderLayer == MyLayerManager.instance.layerBuilding) canCollide = true;
        if (coliderLayer == MyLayerManager.instance.layerTree) canCollide = true;

        if (!canCollide) return;

        this.collideObjects.Add(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        this.collideObjects.Remove(other.gameObject);
    }

}
