using UnityEngine;

public abstract class ResourceCtrl : PoolObj
{
    [Header("Resource")]
    public BuildingType buildingType = BuildingType.resource;
}
