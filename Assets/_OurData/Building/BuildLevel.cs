using System.Collections.Generic;
using UnityEngine;

public class BuildLevel : SaiBehaviour
{
    [SerializeField] protected List<Transform> levels;
    [SerializeField] protected int currentLevel = 0;

    private void OnEnable()
    {
        this.ShowBuilding();
        InvokeRepeating("ShowNextBuild", 3, 2);
    }

    protected override void LoadComponents()
    {
        this.LoadLevels();
    }

    protected virtual void LoadLevels()
    {
        if (this.levels.Count > 0) return;
        Transform buildTran = transform.Find("Buildings");
        foreach (Transform child in buildTran)
        {
            this.levels.Add(child);
            child.gameObject.SetActive(false);
        }

        Debug.Log(transform.name + ": LoadBuildings");
    }

    /// <summary>
    /// Call from InvokeRepeating
    /// </summary>
    protected virtual void ShowNextBuild()
    {
        if (this.currentLevel >= this.levels.Count - 1) return;

        this.currentLevel++;
        this.ShowBuilding();
    }

    protected virtual void ShowBuilding()
    {
        this.HideLastBuild();
        Transform currentBuild = this.levels[this.currentLevel];
        currentBuild.gameObject.SetActive(true);
    }

    protected virtual void HideLastBuild()
    {
        int lastBuildIndex = this.currentLevel - 1;
        if (lastBuildIndex < 0) return;
        Transform lastBuild = this.levels[lastBuildIndex];
        lastBuild.gameObject.SetActive(false);
    }
}
