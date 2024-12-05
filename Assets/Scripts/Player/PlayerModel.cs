public class PlayerModel
{
    public Wallet Wallet { get; }

    public PlayerModel()
    {
        Wallet = new Wallet();
    }

    public void Initialize(float initialMoney)
    {
        Wallet.AddFunds(initialMoney);
    }
}