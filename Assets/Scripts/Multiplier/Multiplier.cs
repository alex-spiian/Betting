using System;
using TMPro;
using UnityEngine;

public class Multiplier : MonoBehaviour
{
    public event Action Score;

    [SerializeField]
    private SpriteRenderer _spriteRenderer;
    
    [SerializeField]
    private TextMeshPro _multiplierLabel;

    private float _multiplier;
    
    public void Initialize(float multiplier, Color color)
    {
        _multiplier = multiplier;
        _multiplierLabel.text = _multiplier.ToString();
        _spriteRenderer.color = color;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.TryGetComponent<Projectile>(out var projectile))
        {
            Score?.Invoke();
            projectile.Hide();
        }
    }
}