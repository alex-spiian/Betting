using System;

public interface IInputHandler
{
    public event Action<ColorType, float> BetValidated;
    public event Action<ColorType> BetPlacing;
    public event Action<float> BetAmountChanged;
    public event Action<int> BalanceToppedUp;

    public void Initialize();

    public void OnBetValidated(ColorType type, float currentBet);
}