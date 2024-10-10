using UnityEngine;

public class BuildDestroyable : SaiBehaviour
{
    public virtual void Destroy()
    {
        PrefabManager.instance.Destroy(transform);
    }
}