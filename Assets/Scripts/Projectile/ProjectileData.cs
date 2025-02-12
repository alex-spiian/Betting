using System;
using UnityEngine;

[Serializable]
public class ProjectileData
{
    [field:SerializeField] public Projectile Prefab { get; private set; }
    [field:SerializeField] public ColorType Type { get; private set; }
}