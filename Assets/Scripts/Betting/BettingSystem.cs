using VitalRouter;

public class BettingSystem
{
    private float _currentBet;
    private float _betStep;
    private readonly PlayerModel _playerModel;
    private readonly IInputHandler _inputHandler;

    public BettingSystem(PlayerModel playerModel, IInputHandler inputHandler)
    {
        _playerModel = playerModel;
        _inputHandler = inputHandler;
        
        _inputHandler.BetAmountChanged += OnBetChanged;
        _inputHandler.BetPlacing += PlaceBet;
    }

    public bool TryValidateRecharging(int amount)
    {
        if (amount > 0)
        {
            _playerModel.Wallet.AddFunds(amount);
            return true;
        }

        return false;
    }

    public bool CanBet()
    {
        return _playerModel.Wallet.Money >= _currentBet;
    }

    private void OnBetChanged(float amount)
    {
        if (amount <= 0)
            return;
            
        _currentBet = amount;
    }
    

    private void PlaceBet(ColorType type)
    {
        if (_playerModel.Wallet.TryDeduct(_currentBet))
        {
            _inputHandler.OnBetValidated(type, _currentBet);
        }
    }
}