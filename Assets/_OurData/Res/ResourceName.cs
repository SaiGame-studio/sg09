using System;

public class ResNameParser
{
    public static ResourceName FromString(string name)
    {
        //name = name.ToLower();
        return (ResourceName)Enum.Parse(typeof(ResourceName), name);
    }
}



public enum ResourceName
{
    noResource = 0,

    //Money
    gold = 1,
    diamond = 2,

    //Material Level 1
    water = 1000,
    logwood = 1001,
    ironOre = 1002,

    //Material Level 2
    blank = 2001,
    ironIgnot = 2002,

}
