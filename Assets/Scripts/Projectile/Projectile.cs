using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [field:SerializeField]
    public ColorType Type { get; private set; }
    
    private event Action<Projectile> _onProjectileHit;
    
    public void Initialize(Action<Projectile> onProjectileHit)
    {
        _onProjectileHit = onProjectileHit;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        _onProjectileHit?.Invoke(this);
    }
}