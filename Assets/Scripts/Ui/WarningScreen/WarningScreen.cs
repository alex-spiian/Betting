
using UnityEngine;
using UnityEngine.UI;

public class WarningScreen : UIScreen
{
    [SerializeField] private Button _topUpButton;
    

    private void Awake()
    {
        _topUpButton.onClick.AddListener(OnTopUp);
    }

    private void OnDestroy()
    {
        _topUpButton.onClick.AddListener(OnTopUp);
    }

    private void OnTopUp()
    {
        Close();
        ScreensManager.OpenScreen<PaymentScreen>();
    }
}