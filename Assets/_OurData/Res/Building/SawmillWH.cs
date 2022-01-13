public class SawmillWH : Warehouse
{
    public override ResHolder ResNeed2Move()
    {
        ResHolder resHolder = this.GetResource(ResourceName.blank);
        if (resHolder.Current() > 0) return resHolder;
        return null;
    }

    public override ResHolder IsNeedRes(Resource res)
    {
        if (res.name != ResourceName.logwood) return null;

        ResHolder resHolder = this.GetResource(res.name);
        if (resHolder == null) return null;
        if (resHolder.IsMax()) return null;
        return resHolder;
    }
}
