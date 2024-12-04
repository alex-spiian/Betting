using System.Collections.Generic;
using System.Linq;
using Pools;
using UnityEngine;
using Random = UnityEngine.Random;

public class ProjectileSpawner : MonoBehaviour
{
    [SerializeField]
    private ProjectileData[] _projectilesData;
    
    [SerializeField]
    private Transform[] _spawnPoints;

    private readonly Dictionary<ColorType, MonoBehaviourPool<Projectile>> _poolsDictionary = new ();

    public void Spawn(ColorType projectileType)
    {
        var spawnPoint = GetSpawnPoint();
        var projectilePool = GetProjectilePool(projectileType);
        var projectile = projectilePool.Take();
        projectile.Initialize(OnProjectileHit);
        projectile.transform.position = spawnPoint.position;
    }

    private Projectile GetProjectilePrefab(ColorType type)
    {
        var requiredProjectile = _projectilesData.FirstOrDefault(projectile => projectile.Type == type);
        if (requiredProjectile == null)
        {
            Debug.LogError("Required Projectile is null. Provide Projectile Data");
        }
        
        return requiredProjectile.Prefab;
    }

    private Transform GetSpawnPoint()
    {
        return _spawnPoints[Random.Range(0, _spawnPoints.Length)];
    }
    
    private MonoBehaviourPool<Projectile> GetProjectilePool(ColorType type)
    {
        if (HasCreatedPool(type))
        {
            return _poolsDictionary[type];
        }

        var projectilePrefab = GetProjectilePrefab(type);
        var projectilePool = new MonoBehaviourPool<Projectile>(projectilePrefab, transform);
        
        _poolsDictionary.Add(type, projectilePool);
        return projectilePool;
    }

    private void OnProjectileHit(Projectile projectile)
    {
        var projectilePool = GetProjectilePool(projectile.Type);
        projectilePool.Release(projectile);
    }

    private bool HasCreatedPool(ColorType type)
    {
        return _poolsDictionary.ContainsKey(type);
    }
}