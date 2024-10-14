public class ForestHutWH : Warehouse
{
    public override ResHolder ResNeed2Move()
    {
        ResHolder resHolder = this.GetRes(ResourceName.logwood);
        if (resHolder.Current() > 0) return resHolder;
        return null;
    }
}
