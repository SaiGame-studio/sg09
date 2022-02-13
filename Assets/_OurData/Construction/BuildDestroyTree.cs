

public class BuildDestroyTree : BuildDestroyable
{
    public override void Destroy()
    {
        TreeManager.instance.TreeRemove(gameObject);
        base.Destroy();
    }
}
