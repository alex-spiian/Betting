using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InputHandler : MonoBehaviour, IInputHandler
{
    public event Action<ColorType, float> BetValidated;
    public event Action<ColorType> BetPlacing;
    public event Action<float> BetAmountChanged;
    public event Action<int> BalanceToppedUp;

    [SerializeField] private TMP_Dropdown _dropdown;
    [SerializeField] private BetButtonData[] _betButtons;
    [SerializeField] private TMP_InputField _inputField;
    [SerializeField] private Button _topUpButton;
    
    public void Initialize()
    {
        _dropdown.onValueChanged.AddListener(OnDropdownValueChanged);
        _topUpButton.onClick.AddListener(OnToppedUp);

        foreach (var buttonData in _betButtons)
        {
            buttonData.Button.onClick.AddListener(() => OnBetPlacing(buttonData.Type));
        }
        
        OnDropdownValueChanged(0);
    }

    private void OnDestroy()
    {
        _dropdown.onValueChanged.RemoveListener(OnDropdownValueChanged);
        _topUpButton.onClick.RemoveListener(OnToppedUp);

        foreach (var buttonData in _betButtons)
        {
            buttonData.Button.onClick.RemoveListener(() => OnBetPlacing(buttonData.Type));
        }
    }

    public void OnBetValidated(ColorType type, float currentBet)
    {
        BetValidated?.Invoke(type, currentBet);
    }
    
    private void OnBetPlacing(ColorType colorType)
    {
        BetPlacing?.Invoke(colorType);
    }
    
    private void OnDropdownValueChanged(int index)
    {
        var selectedOption = _dropdown.options[index].text;
        var betAmount = selectedOption.ParseBetAmount();
        BetAmountChanged?.Invoke(betAmount);
    }

    private void OnToppedUp()
    {
        var amount = 0;
        if (string.IsNullOrEmpty(_inputField.text))
        {
            BalanceToppedUp?.Invoke(amount);
            return;
        }

        amount = Convert.ToInt32(_inputField.text);
        BalanceToppedUp?.Invoke(amount);
    }
}