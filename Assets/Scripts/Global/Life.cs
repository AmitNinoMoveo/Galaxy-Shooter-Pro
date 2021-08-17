using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Life : GlobalInfo
{
    [SerializeField]
    private int _maxLife = 1;
    public int MaxLife
    {
        get => _maxLife;
        set
        {
            if (value >= 1)
            {
                _maxLife = value;
            }
        }
    }
    [SerializeField]
    private int _currentLife = 1;
    public int CurrentLife
    {
        get => _currentLife;
        set
        {
            if (value >= 0 || value <= this.MaxLife)
            {
                _currentLife = value;
            }
        }
    }
    [SerializeField]
    private bool _isAlive = true;
    public bool IsAlive
    {
        get => _isAlive;
        set
        {
            _isAlive = value;
        }
    }
    public Life(int maxLife = 1)
    {
        this.MaxLife = maxLife;
        this.CurrentLife = maxLife;
    }
    public void decreseCurrentLife(int amount = 1)
    {
        if (amount >= this.CurrentLife)
        {
            this.IsAlive = false;
            this.CurrentLife = 0;
            return;
        }
        else
        {
            this.CurrentLife -= amount;
        }
    }
    public void increaseCurrentLife(int amount)
    {
        if ((amount + this.CurrentLife) >= this.MaxLife)
        {
            this.CurrentLife = this.MaxLife;
            return;
        }
        else
        {
            this.CurrentLife += amount;
        }
    }
}
