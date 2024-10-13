using UnityEngine;

public class GodInput : SaiBehaviour
{
    public GodModeCtrl godModeCtrl;
    public bool isMouseRotating = false;
    public Vector2 mouseScroll = new Vector2();
    public Vector3 mouseReference = new Vector3();
    public Vector3 mouseRotation = new Vector3();

    protected virtual void Update()
    {
        this.InputHandle();
        this.MouseRotation();
        this.ChoosePlace2Build();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadGetModeCtrl();
    }

    protected virtual void LoadGetModeCtrl()
    {
        if (this.godModeCtrl != null) return;
        this.godModeCtrl = GetComponent<GodModeCtrl>();
        Debug.LogWarning(transform.name + ": LoadGetModeCtrl", gameObject);
    }

    protected virtual void InputHandle()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        float y = Input.mouseScrollDelta.y * -1;
        bool leftShift = Input.GetKey(KeyCode.LeftShift);

        this.godModeCtrl.godMovement.camMovement.x = x;
        this.godModeCtrl.godMovement.camMovement.z = z;
        this.godModeCtrl.godMovement.camMovement.y = y;
        this.godModeCtrl.godMovement.speedShift = leftShift;
    }

    protected virtual void MouseRotation()
    {
        //TODO: move to InputManager
        this.isMouseRotating = Input.GetKey(KeyCode.Mouse1);
        if (Input.GetKeyDown(KeyCode.Mouse1)) this.mouseReference = Input.mousePosition;

        if (this.isMouseRotating)
        {
            this.mouseRotation = (Input.mousePosition - this.mouseReference);
            this.mouseRotation.y = -(this.mouseRotation.x + this.mouseRotation.y);
            this.mouseReference = Input.mousePosition; 
        }
        else
        {
            this.mouseRotation = Vector3.zero;
        }

        this.godModeCtrl.godMovement.camRotation.y = this.mouseRotation.x;
    }

    protected virtual void ChoosePlace2Build()
    {
        if (!ConstructionSpawnerCtrl.Instance.Creator.isBuilding) return;
        if (!Input.GetKeyUp(KeyCode.Mouse0)) return; //TODO: move to InputManager
        ConstructionSpawnerCtrl.Instance.Creator.CurrentBuildPlace();
    }
}
