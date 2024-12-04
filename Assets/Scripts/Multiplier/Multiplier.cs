using TMPro;
using Unity.Android.Gradle.Manifest;
using UnityEngine;

public class Multiplier : MonoBehaviour
{
    public ColorType Type { get; private set; }
    public float Value { get; private set; }

    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private TextMeshPro _multiplierLabel;

    public void Initialize(ColorType type, float multiplier, Color color)
    {
        Type = type;
        Value = multiplier;
        
        _multiplierLabel.text = Value.ToString();
        _spriteRenderer.color = color;
    }

    public void ShowAnimation()
    {
        Debug.Log($"Animation of {Type} {Value}");
    }
}