using System.Collections.Generic;
using UnityEngine;

public class PrefabManager : SaiBehaviour
{
    public static PrefabManager instance;
    [SerializeField] protected List<Transform> prefabs;

    protected override void Awake()
    {
        base.Awake();
        if (PrefabManager.instance != null) Debug.LogError("Only 1 PrefabManager allow");
        PrefabManager.instance = this;
    }

    protected override void Start()
    {
        base.Start();
        this.HideAllPrefabs();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadPrefabs();
    }

    protected virtual void LoadPrefabs()
    {
        if (this.prefabs.Count > 0) return;
        foreach (Transform child in transform)
        {
            this.prefabs.Add(child);
        }

        Debug.Log(transform.name + ": LoadPrefabs", gameObject);
    }

    protected virtual void HideAllPrefabs()
    {
        foreach (Transform prefab in this.prefabs)
        {
            prefab.gameObject.SetActive(false);
        }
    }

    public virtual Transform Instantiate(string prefabName)
    {
        Transform prefab = this.Get(prefabName);
        GameObject newObj = Instantiate(prefab.gameObject);
        newObj.name = prefab.name;

        return newObj.transform;
    }

    public virtual void Destroy(Transform transform)
    {
        Destroy(transform.gameObject);
    }

    public virtual Transform Get(string prefabName)
    {
        foreach (Transform prefab in this.prefabs)
        {
            if (prefab.name != prefabName) continue;
            return prefab;
        }

        return null;
    }
}
