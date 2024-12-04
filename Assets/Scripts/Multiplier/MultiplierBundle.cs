using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class MultiplierBundle : MonoBehaviour
{
    public event Action<float> Score;
    
    private ColorType _type;
    private readonly List<Multiplier> _multipliers = new();

    public void Initialize(ColorType type)
    {
        _type = type;
    }

    public void Add(Multiplier multiplier)
    {
        _multipliers.Add(multiplier);
    }
    
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.TryGetComponent<Projectile>(out var projectile))
        {
            var requiredMultiplier = _multipliers.FirstOrDefault(multiplier => multiplier.Type == projectile.Type);

            if (projectile.Type == requiredMultiplier.Type)
            {
                Score?.Invoke(requiredMultiplier.Value);
                projectile.Hide();
                requiredMultiplier.ShowAnimation();
            }
        }
    }
}