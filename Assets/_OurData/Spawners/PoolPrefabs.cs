using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PoolPrefabs<T> : SaiBehaviour where T : MonoBehaviour
{
    [SerializeField] protected List<T> prefabs = new();
    public List<T> Prefabs => prefabs;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadPrefabs();
        this.HidePrefabs();
    }

    protected virtual void LoadPrefabs()
    {
        if (this.prefabs.Count > 0) return;
        foreach (Transform child in transform)
        {
            T classPrefab = child.GetComponent<T>();
            if (classPrefab != null) this.prefabs.Add(classPrefab);
        }
        Debug.Log(transform.name + ": LoadPrefabs", gameObject);
    }

    protected virtual void HidePrefabs()
    {
        foreach (T prefab in this.prefabs)
        {
            prefab.gameObject.SetActive(false);
        }
    }

    public virtual T GetRandom()
    {
        int rand = Random.Range(0, this.prefabs.Count);
        return this.prefabs[rand];
    }

    public virtual T GetByName(string prefabName)
    {
        foreach (T prefab in this.prefabs)
        {
            if (prefab.name != prefabName) continue;
            return prefab;
        }

        return null;
    }
}
