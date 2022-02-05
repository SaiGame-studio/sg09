using UnityEngine;

public class BuildWarehouse : BuildBuilding
{
    protected override void LoadResRequires()
    {
        if (this.resRequires.Count > 0) return;
        this.resRequires.Add(new Resource { name = ResourceName.logwood, number = 2 });
        this.resRequires.Add(new Resource { name = ResourceName.blank, number = 5 });
        Debug.Log(transform.name + ": LoadResRequires", gameObject);
    }
}
