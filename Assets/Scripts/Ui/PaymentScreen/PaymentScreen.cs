using DG.Tweening;
using TMPro;
using UnityEngine;

public class PaymentScreen : UIScreen
{
    private const string FAILED_MESSAGE = "Payment has failed. Try again!";
    
    [SerializeField] private TextMeshProUGUI _messageLabel;
    [SerializeField] private float _messageAnimationDuretion = 3;

    public override void Tick()
    {
        ResetLabel(_messageLabel);
    }

    public void OnPaymentValidated(bool isSuccessful)
    {
        if (isSuccessful)
        {
            ScreensManager.CloseAllScreens();
            return;
        }

        ShowMessage();
    }

    private void ShowMessage()
    {
        _messageLabel.text = FAILED_MESSAGE;
        var currentColor = _messageLabel.color;
        currentColor.a = 0;
        _messageLabel.DOColor(currentColor, _messageAnimationDuretion).OnComplete(() => ResetLabel(_messageLabel));
    }

    private void ResetLabel(TextMeshProUGUI textLabel)
    {
        _messageLabel.text = "";
        var textLabelColor = textLabel.color;
        textLabelColor.a = 1;
        textLabel.color = textLabelColor;
    }
}