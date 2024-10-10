using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class SliderHp : SliderAbstact
{
    protected void FixedUpdate()
    {
        this.UpdateSlider();
    }

    protected virtual void UpdateSlider()
    {
        this.slider.value = this.GetValue();
    }

    protected abstract float GetValue();

}
