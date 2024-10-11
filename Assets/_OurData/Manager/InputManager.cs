using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : SaiSingleton<InputManager>
{
    [SerializeField] protected bool isHoldingShift = false;
    public bool IsHoldingShift => isHoldingShift;

    protected virtual void Update()
    {
        this.CheckingHoldingShift();
    }

    protected virtual void CheckingHoldingShift()
    {
        this.isHoldingShift = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
    }
}
