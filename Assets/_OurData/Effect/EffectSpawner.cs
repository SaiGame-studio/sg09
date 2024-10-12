using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectSpawner : Spawner<EffectCtrl>
{
    public virtual EffectCtrl Spawn(EffectName effectName, Vector3 postion)
    {
        EffectCtrl prefab = this.PoolPrefabs.GetByName(effectName.ToString());
        EffectCtrl newObject = this.Spawn(prefab, postion);
        return newObject;
    }
}
