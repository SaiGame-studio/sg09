using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(Rigidbody))]
public class LimitRadius : SaiBehaviour
{
    [Header("Limit Radius")]
    [SerializeField] protected float buildRadius = 7f;
    [SerializeField] protected SphereCollider _collider;
    [SerializeField] protected Rigidbody _rigidbody;
    public List<GameObject> collideObjects;

    protected override void OnEnable()
    {
        base.OnEnable();
        this.ResetColliderObjects();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCollider();
        this.LoadRigibody();
    }

    protected virtual void LoadCollider()
    {
        if (this._collider != null) return;
        this._collider = GetComponent<SphereCollider>();
        this._collider.radius = this.buildRadius;
        this._collider.isTrigger = true;
        Debug.Log(transform.name + ": LoadCollider", gameObject);
    }

    protected virtual void LoadRigibody()
    {
        if (this._rigidbody != null) return;
        this._rigidbody = GetComponent<Rigidbody>();
        this._rigidbody.useGravity = false;
        Debug.Log(transform.name + ": LoadRigibody", gameObject);
    }

    public virtual bool IsCollided()
    {
        if (collideObjects.Count < 1) return false;

        //Check if building collided
        foreach (GameObject colliderObj in this.collideObjects)
        {
            if (colliderObj.layer == MyLayerManager.instance.layerBuilding) return true;
        }

        GameObject colObj;
        int i = 0;
        do
        {
            colObj = this.collideObjects[i];
            if (colObj.layer == MyLayerManager.instance.layerTree)
            {
                this.collideObjects.RemoveAt(i);
                this.CleanObject(colObj);
                i = 0;
                continue;
            }
            i++;
        } while (i < this.collideObjects.Count);

        return false;
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

    protected virtual void CleanObject(GameObject other)
    {
        TreeManager.instance.TreeRemove(other);
        PrefabManager.instance.Destroy(other.transform);
    }

    protected virtual void ResetColliderObjects()
    {
        this.collideObjects.Clear();
    }

}
