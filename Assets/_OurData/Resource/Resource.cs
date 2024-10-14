using System;

[Serializable]
public class Resource
{
    public string name;
    public ResourceName codeName;
    public int number = 0;
    public int adding = 0;
    public int deducting = 0;
    public int max = int.MaxValue;
    public Resource(ResourceName codeName, int number)
    {
        this.codeName = codeName;
        this.number = number;
        this.name = this.codeName.ToString();
    }

    public bool Add(int number)
    {
        if (!this.TryToAdd(number)) return false;
        this.number += number;
        return true;
    }

    public bool TryToAdd(int number)
    {
        int newNumber = this.number + number;
        if (newNumber > this.max) return false;
        return true;
    }

    public bool Remove(int number)
    {
        if (!this.TryToRemove(number)) return false;
        this.number -= number;
        return true;
    }

    public bool TryToRemove(int number)
    {
        int newNumber = this.number - number;
        if (newNumber < 0) return false;
        return true;
    }

}
