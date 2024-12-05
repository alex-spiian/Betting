using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float CurrentBet { get; private set; }

    [field:SerializeField]
    public ColorType Type { get; private set; }
    
    private event Action<Projectile> _onProjectileHit;
    
    public void Initialize(Action<Projectile> onProjectileHit, float currentBet)
    {
        CurrentBet = currentBet;
        _onProjectileHit = onProjectileHit;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        _onProjectileHit?.Invoke(this);
    }
}