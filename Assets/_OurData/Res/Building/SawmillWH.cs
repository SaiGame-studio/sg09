public class SawmillWH : Warehouse
{
    public override ResHolder ResNeed2Move()
    {
        ResHolder resHolder = this.GetResource(ResourceName.blank);
        if (resHolder.Current() > 0) return resHolder;
        return null;
    }

    public override ResHolder IsNeedRes()
    {
        ResHolder resHolder = this.GetResource(ResourceName.logwood);
        if (resHolder.IsMax()) return null;
        return resHolder;
    }
}
