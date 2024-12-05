using System;

public class Wallet
{
    public Action<float> MoneyChanged;

    public float Money { get; private set; }

    public bool TryDeduct(float amount)
    {
        if (HasEnoughMoney(amount) && amount > 0)
        {
            Money -= amount;
            MoneyChanged?.Invoke(Money);
            return true;
        }
        return false;
    }

    public bool HasEnoughMoney(float amount)
    {
        return Money >= amount;
    }

    public void AddFunds(float multiplier, float currentBet)
    {
        var amount = currentBet * multiplier;
        if (amount > 0)
        {
            Money += amount;
            MoneyChanged?.Invoke(Money);
        }
    }
    
    public void AddFunds(float amount)
    {
        if (amount > 0)
        {
            Money += amount;
            MoneyChanged?.Invoke(Money);
        }
    }
}