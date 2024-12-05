using System;

public class Wallet
{
    public Action<float> MoneyChanged;

    private float _money;

    public bool TryDeduct(float amount)
    {
        if (HasEnoughMoney(amount) && amount > 0)
        {
            _money -= amount;
            MoneyChanged?.Invoke(_money);
            return true;
        }
        return false;
    }

    public bool HasEnoughMoney(float amount)
    {
        return _money >= amount;
    }

    public void AddFunds(float multiplier, float currentBet)
    {
        var amount = currentBet * multiplier;
        if (amount > 0)
        {
            _money += amount;
            MoneyChanged?.Invoke(_money);
        }
    }
    
    public void AddFunds(float amount)
    {
        if (amount > 0)
        {
            _money += amount;
            MoneyChanged?.Invoke(_money);
        }
    }
}