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
    [SerializeField] protected int max = int.MaxValue;
    public int Max => max;

    [SerializeField] protected int willAdd = 0;
    [SerializeField] protected int willDeduct = 0;

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

    public bool Generate(int number)
    {
        if (!this.TryToAdd(number)) return false;
        this.number += number;
        return true;
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
        return this.max <= 0 || newNumber <= this.max;
    }


    public bool Deduct(int number)
    {
        if (!this.TryToDeduct(number)) return false;
        this.number -= number;
        return true;
    }

    public bool TryToDeduct(int number)
    {
        int newNumber = this.number - number;
        if (newNumber < 0) return false;
        return true;
    }

    public bool IsMax()
    {
        if (this.max == 0) return false;
        return this.NumberFinal() >= this.max;
    }

    public bool IsEmplty()
    {
        return this.NumberFinal() == 0;
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

    public void WillDeduct(int number)
    {
        this.willDeduct += number;
    }

    public void WillDeduct(int number, WorkerCtrl worker)
    {
        this.WillDeduct(worker.inventory.Taking(number));
    }

    public void Deducted(int number, WorkerCtrl worker)
    {
        this.Deducted(worker.inventory.Taking(number));
    }

    public void Deducted(int number)
    {
        this.willDeduct -= number;
    }

    public void WillAdd(int number)
    {
        this.willAdd += number;
    }

    public void Added(int number)
    {
        this.willAdd -= number;
    }

    public int NumberFinal()
    {
        return this.number - this.willDeduct + this.willAdd;
    }
}
