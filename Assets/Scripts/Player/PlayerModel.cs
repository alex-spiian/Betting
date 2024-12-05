public class PlayerModel
{
    public Wallet Wallet { get; private set; }

    public PlayerModel()
    {
        Wallet = new Wallet();
    }

    public void Initialize(float initialMoney)
    {
        Wallet.AddFunds(initialMoney);
    }
}