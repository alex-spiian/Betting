using UnityEngine;

public class BettingSystem
{
    public PlayerModel PlayerModel { get; private set; }

    private float _currentBet;
    private float _betStep;
    private readonly IInputHandler _inputHandler;

    public BettingSystem(PlayerModel playerModel, IInputHandler inputHandler)
    {
        PlayerModel = playerModel;
        _inputHandler = inputHandler;
        
        _inputHandler.BetAmountChanged += OnBetChanged;
        _inputHandler.BetPlacing += PlaceBet;
    }

    private void OnBetChanged(float amount)
    {
        if (amount <= 0)
            return;
            
        _currentBet = amount;
    }

    private void OnIncreaseBet()
    {
        IncreaseBet(_betStep);
    }

    private void OnDecreaseBet()
    {
        DecreaseBet(_betStep);
    }

    private void IncreaseBet(float amount)
    {
        if (amount <= 0)
            return;
        
        _currentBet += amount;
    }

    private void DecreaseBet(float amount)
    {
        if (amount <= 0 || _currentBet - amount <= 0)
            return;
        
        _currentBet -= amount;
    }

    private void PlaceBet(ColorType type)
    {
        if (PlayerModel.Wallet.TryDeduct(_currentBet))
        {
            _inputHandler.OnBetValidated(type, _currentBet);
            Debug.Log($"BEt {_currentBet}");
            return;
        }
        
        Debug.Log("Not enough balance or invalid bet.");
    }
}