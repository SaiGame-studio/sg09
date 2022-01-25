using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildBuilding : SaiBehaviour
{
    [Header("Build")]
    [SerializeField] protected float percent = 0f;
    [SerializeField] protected float timer = 0f;
    [SerializeField] protected float delay = 0.05f;
    [SerializeField] protected List<string> buildNames;

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        this.Building();
        if (this.percent >= 100f)  this.FinishBuild();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        this.BuildReset();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadBuildNames();
    }

    protected virtual void Building()
    {
        this.timer += Time.fixedDeltaTime;
        if (this.timer < this.delay) return;
        this.timer = 0;
        this.percent += 1;
    }

    protected virtual Transform FinishBuild()
    {
        Transform newBuild = PrefabManager.instance.Instantiate(this.GetBuildName());
        newBuild.position = transform.position;
        newBuild.gameObject.SetActive(true);

        PrefabManager.instance.Destroy(transform);
        return newBuild;
    }

    protected virtual string GetBuildName()
    {
        int rand = Random.Range(0, this.buildNames.Count);
        return this.buildNames[rand];
    }

    protected virtual void BuildReset()
    {
        this.percent = 0;
        this.timer = 0;
    }

    protected virtual void LoadBuildNames()
    {
        if (this.buildNames.Count > 0) return;
        string name = transform.name.Replace("Build", "");
        this.buildNames.Add(name);
        Debug.Log(transform.name + ": LoadBuildNames", gameObject);
    }
}
