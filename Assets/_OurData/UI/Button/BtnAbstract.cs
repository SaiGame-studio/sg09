using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class BtnAbstract : SaiBehaviour
{
    [SerializeField] protected Button button;

    protected override void Start()
    {
        base.Start();
        this.AddOnClickEvent();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadButton();
    }

    protected virtual void LoadButton()
    {
        if (this.button != null) return;
        this.button = GetComponent<Button>();
        Debug.Log(transform.name + ": LoadButton", gameObject);
    }

    protected virtual void AddOnClickEvent()
    {
        this.button.onClick.AddListener(this.OnClick);
    }

    protected abstract void OnClick();
}
