using UnityEngine;

public class GodMovement : SaiBehaviour
{
    public GodModeCtrl godModeCtrl;
    public float speed = 27f;
    public bool speedShift = false;
    public float minY = 4f;
    public float maxY = 70f;
    public Vector3 camRotation = new Vector3(0, 0, 0);
    public Vector3 camMovement = new Vector3(0, 0, 0);
    public Vector3 camView = new Vector3(45f, 0, 0);

    protected override void Update()
    {
        base.Update();
        this.Moving();
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
        Debug.Log(transform.name + ": LoadGetModeCtrl", gameObject);
    }

    protected virtual void Moving()
    {
        float speed = this.speed;
        if (this.speedShift) speed += this.speed * 2;

        Vector3 movement = this.camMovement;
        movement.x *= speed;
        movement.z *= speed;
        movement.y *= speed * 7;

        Vector3 oldPos = transform.position;
        transform.Translate(movement * Time.deltaTime);
        Vector3 newPos = transform.position;

        if (newPos.y < this.minY)
        {
            newPos.y = this.minY;
            transform.position = newPos;
        }

        if (newPos.y > this.maxY)
        {
            newPos.y = this.maxY;
            transform.position = newPos;
        }

        transform.Rotate(this.camRotation);

    }
}
