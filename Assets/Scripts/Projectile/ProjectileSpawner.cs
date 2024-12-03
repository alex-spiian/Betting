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

    private readonly Dictionary<ProjectileType, MonoBehaviourPool<Projectile>> _poolsDictionary = new ();

    public void Spawn(ProjectileType projectileType)
    {
        var spawnPoint = GetSpawnPoint();
        var projectilePool = GetProjectilePool(projectileType);
        var projectile = projectilePool.Take();
        projectile.Initialize(OnProjectileHit);
        projectile.transform.position = spawnPoint.position;
    }

    private Projectile GetProjectilePrefab(ProjectileType projectileType)
    {
        var requiredProjectile = _projectilesData.FirstOrDefault(projectile => projectile.Type == projectileType);
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
    
    private MonoBehaviourPool<Projectile> GetProjectilePool(ProjectileType projectileType)
    {
        if (HasCreatedPool(projectileType))
        {
            return _poolsDictionary[projectileType];
        }

        var projectilePrefab = GetProjectilePrefab(projectileType);
        var projectilePool = new MonoBehaviourPool<Projectile>(projectilePrefab, transform);
        
        _poolsDictionary.Add(projectileType, projectilePool);
        return projectilePool;
    }

    private void OnProjectileHit(Projectile projectile)
    {
        var projectilePool = GetProjectilePool(projectile.Type);
        projectilePool.Release(projectile);
    }

    private bool HasCreatedPool(ProjectileType projectileType)
    {
        return _poolsDictionary.ContainsKey(projectileType);
    }
}