using UnityEngine;

public class FpsCounter : TextAbstact
{
    private float deltaTime = 0.0f;

    void Update()
    {
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        float fps = 1.0f / deltaTime;
        this.textPro.text = Mathf.Ceil(fps).ToString();
    }
}
