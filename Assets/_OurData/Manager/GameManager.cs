using UnityEngine;

public class GameManager : SaiBehaviour
{
    protected override void Awake()
    {
        base.Awake();
        QualitySettings.vSyncCount = 1; 
        Application.targetFrameRate = 120; 
    }
}
