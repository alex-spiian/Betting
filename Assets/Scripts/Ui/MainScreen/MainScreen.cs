using TMPro;
using UnityEngine;

public class MainScreen : UIScreen
{
    [SerializeField] private TextMeshProUGUI _currentBetLabel;
    [SerializeField] private TextMeshProUGUI _currentMoneyLabel;
    

    public override void Tick()
    {
    }

    public void RefreshBetAmount(float amount)
    {
        _currentBetLabel.text = amount.ToString();
    }

    public void RefreshMoneyAmount(float amount)
    {
        _currentMoneyLabel.text = amount.ToString();
    }
}