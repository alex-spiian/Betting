using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class RewardLabel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _rewardAmountLabel;
    [SerializeField] private Color _greenColor;
    [SerializeField] private Color _redColor;
    
    [SerializeField] private float _fadeDuration = 0.2f;
    [SerializeField] private float _scaleDuration = 0.2f;
    [SerializeField] private float _activeStateDuration = 1f;

    private Action<RewardLabel> _callBack;
    private CancellationTokenSource _cancellationTokenSource = new();

    public void Show(float multiplier, float bet, Action<RewardLabel> callBack)
    {
        _cancellationTokenSource = new CancellationTokenSource();
        
        _callBack = callBack;
        var color = multiplier >= 1 ? _greenColor : _redColor;
        _rewardAmountLabel.text = $"+{(bet * multiplier)}";
        _rewardAmountLabel.color = color;
        
        ProcessAnimation();
    }

    private async void ProcessAnimation()
    {
        await UniTask.WaitForSeconds(_activeStateDuration, cancellationToken: _cancellationTokenSource.Token);
        if (_cancellationTokenSource.Token.IsCancellationRequested)
            return;
        
        var initialColor = _rewardAmountLabel.color;
        initialColor.a = 0;
        var sequence = DOTween.Sequence();
        sequence.Append(_rewardAmountLabel.transform.DOScale(Vector3.one, _scaleDuration));
        sequence.Append(_rewardAmountLabel.DOColor(initialColor, _fadeDuration));
        sequence.Play().OnComplete(Reset);
    }

    private void Reset()
    {
        _rewardAmountLabel.text = string.Empty;
        var color = _rewardAmountLabel.color;
        color.a = 1;
        _rewardAmountLabel.color = color;
        _callBack?.Invoke(this);
    }

    private void OnDisable()
    {
        _cancellationTokenSource.Cancel();
    }
}