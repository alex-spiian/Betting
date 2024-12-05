using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VitalRouter;

[RequireComponent(typeof(BoxCollider2D))]
public class MultiplierBundle : MonoBehaviour
{
    private readonly List<Multiplier> _multipliers = new();
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
                projectile.Hide();
                requiredMultiplier.ShowAnimation();
                Router.Default.PublishAsync(new GotTargetEvent(requiredMultiplier.Type, requiredMultiplier.Value, projectile.CurrentBet));
            }
        }
    }
}