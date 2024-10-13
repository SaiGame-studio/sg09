using UnityEngine;

public class GodModeCtrl : SaiBehaviour
{
    public static GodModeCtrl instance;

    [Header("God Mode")]
    public Camera _camera;
    public GodMovement godMovement;

    protected override void Awake()
    {
        base.Awake();
        if (GodModeCtrl.instance != null) Debug.LogError("Only 1 GodModeCtrl allow");
        GodModeCtrl.instance = this;
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadGodMovement();
        this.LoadCamera();
    }

    protected virtual void LoadGodMovement()
    {
        if (this.godMovement != null) return;
        this.godMovement = GetComponent<GodMovement>();
        Debug.LogWarning(transform.name + ": LoadGodMovement", gameObject);
    }

    protected virtual void LoadCamera()
    {
        if (this._camera != null) return;
        this._camera = transform.Find("Camera").GetComponent<Camera>();
        this._camera.transform.rotation = Quaternion.Euler(this.godMovement.camView.x, this.godMovement.camView.y, this.godMovement.camView.z);
        Debug.LogWarning(transform.name + ": LoadCamera", gameObject);
    }
}
