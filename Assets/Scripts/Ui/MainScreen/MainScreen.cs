using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainScreen : UIScreen
{
    [SerializeField] private TextMeshProUGUI _currentBetLabel;
    [SerializeField] private TextMeshProUGUI _currentMoneyLabel;
    [SerializeField] private Button _topUpButton;

    public override void Tick()
    {
    }

    private void Awake()
    {
        _topUpButton.onClick.AddListener(OnTopUp);
    }

    private void OnDestroy()
    {
        _topUpButton.onClick.RemoveListener(OnTopUp);
    }

    public void RefreshBetAmount(float amount)
    {
        _currentBetLabel.text = amount.ToString();
    }

    public void RefreshMoneyAmount(float amount)
    {
        _currentMoneyLabel.text = amount.ToString();
    }

    private void OnTopUp()
    {
        ScreensManager.OpenScreen<PaymentScreen>();
    }
}