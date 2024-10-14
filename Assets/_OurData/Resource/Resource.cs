using System;
using UnityEngine;

[Serializable]
public class Resource
{
    public string name;
    [SerializeField] protected ResourceName codeName;
    public ResourceName CodeName => codeName;
    [SerializeField] protected int number = 0;
    public int Number => number;
    public int adding = 0;
    public int deducting = 0;
    [SerializeField] protected int max = int.MaxValue;
    public int Max => max;

    public Resource(ResourceName codeName)
    {
        this.codeName = codeName;
        this.name = this.codeName.ToString();
    }

    public Resource(ResourceName codeName, int number)
    {
        this.codeName = codeName;
        this.number = number;
        this.name = this.codeName.ToString();
    }

    public Resource(ResourceName codeName, int number, int max)
    {
        this.codeName = codeName;
        this.number = number;
        this.max = max;
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

    public bool IsMax()
    {
        return this.number >= this.max;
    }

    public void SetMax(int max)
    {
        this.max = max;
    }

    public void SetNumber(int number)
    {
        this.number = number;
    }

    public int TakeAll()
    {
        int all = this.number;
        this.number = 0;
        return all;
    }

}
