using UnityEngine;

public class SaiBehaviour : MonoBehaviour
{

    protected float lastUpdateTime = 0f;

    protected virtual void Reset()
    {
        this.ResetValues();
        this.LoadComponents();
    }

    protected virtual void Awake()
    {
        this.LoadComponents();
    }

    protected virtual void Start()
    {
        this.lastUpdateTime = Time.time;
    }

    protected virtual void LoadComponents()
    {
        //For Overide
    }

    protected virtual void ResetValues()
    {
        //For Overide
    }

    protected virtual float GetElapsedTime()
    {
        float currentTime = Time.time;
        float elapsedTime = currentTime - this.lastUpdateTime;
        this.lastUpdateTime = currentTime;
        return elapsedTime;
    }
}
