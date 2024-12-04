using DG.Tweening;
using TMPro;
using UnityEngine;

public class Multiplier : MonoBehaviour
{
    public ColorType Type { get; private set; }
    public float Value { get; private set; }

    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private TextMeshPro _multiplierLabel;
    
    [SerializeField] private float _scaleMultiplier;
    [SerializeField] private float _animationDuration;
    private Sequence _sequence;

    public void Initialize(ColorType type, float multiplier, Color color)
    {
        Type = type;
        Value = multiplier;
        
        _multiplierLabel.text = Value.ToString();
        _spriteRenderer.color = color;
    }

    public void ShowAnimation()
    {
        _sequence = DOTween.Sequence();
        _sequence.Append(transform.DOScale(Vector3.one * _scaleMultiplier, _animationDuration / 2));
        _sequence.Append(transform.DOScale(Vector3.one, _animationDuration / 2));
        _sequence.Play();
    }

    private void OnDisable()
    {
        _sequence.Kill();
        transform.DOKill();
    }
}