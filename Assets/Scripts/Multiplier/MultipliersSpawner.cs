using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MultipliersSpawner : MonoBehaviour
{
    [SerializeField] private MultiplierData[] _multipliersData;
    [SerializeField] private MultiplierVisualConfig multipliersVisualConfig;
    [SerializeField] private Multiplier multiplierPrefab;
    
    [SerializeField] private float _spawnInterval;

    private readonly List<Multiplier> _targets = new();

    private void Start()
    {
        Generate();
    }

    public void Generate()
    {
        foreach (var targetData in _multipliersData)
        {
            var spawnPosition = targetData.StartSpawnPoint.position;
            foreach (var multiplier in targetData.Multipliers)
            {
                var container = GetContainer(targetData.Type);
                var target = Instantiate(multiplierPrefab, container);
                target.transform.position = spawnPosition;
                target.Initialize(multiplier, multipliersVisualConfig.GetColor(targetData.Type));
                _targets.Add(target);
                spawnPosition.x += _spawnInterval;
            }
        }
    }

    private Transform GetContainer(ColorType type)
    {
        var requiredContainerData = _multipliersData.FirstOrDefault(container => container.Type == type);
        if (requiredContainerData == null)
        {
            Debug.LogError($"Required container type {type} was not found. Provide MultiplierData of {type} type");
        }

        return requiredContainerData.Container;
    }
}