public class ForestHutWH : Warehouse
{
    public override ResHolder ResNeed2Move()
    {
        ResHolder resHolder = this.GetResource(ResourceName.logwood);
        if (resHolder.Current() > 0) return resHolder;

        return null;
    }
}
