public class SawmillWH : Warehouse
{
    public override ResHolder ResNeed2Move()
    {
        ResHolder resHolder = this.GetResource(ResourceName.blank);
        if (resHolder.Current() > 0) return resHolder;
        return null;
    }

    public override ResHolder IsNeedRes(ResourceName resName)
    {
        if (resName != ResourceName.logwood) return null;

        ResHolder resHolder = this.GetResource(resName);
        if (resHolder.IsMax()) return null;
        return resHolder;
    }
}
