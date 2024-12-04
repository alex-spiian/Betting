using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MultipliersSpawner : MonoBehaviour
{
    [SerializeField] private MultiplierData[] _multipliersData;
    [SerializeField] private MultiplierBundle[] _multiplierBundles;
    [SerializeField] private MultiplierVisualConfig multipliersVisualConfig;
    [SerializeField] private Multiplier multiplierPrefab;
    [SerializeField] private float _spawnInterval;
    private readonly List<Multiplier> _multipliers = new();

    public void Generate()
    {
        foreach (var multiplierData in _multipliersData)
        {
            GenerateForMultiplierData(multiplierData);
        }
    }

    private void GenerateForMultiplierData(MultiplierData multiplierData)
    {
        var index = 0;
        var spawnPosition = multiplierData.StartSpawnPoint.position;

        foreach (var multiplierBundle in _multiplierBundles)
        {
            if (index >= multiplierData.Multipliers.Length)
                break;

            CreateMultiplier(multiplierData, multiplierBundle, ref spawnPosition, ref index);
        }
    }

    private void CreateMultiplier(MultiplierData multiplierData, MultiplierBundle multiplierBundle, ref Vector3 spawnPosition, ref int index)
    {
        var container = GetContainer(multiplierData.Type);
        var multiplier = Instantiate(multiplierPrefab, container);
        multiplier.transform.position = spawnPosition;

        var type = multiplierData.Type;
        var value = multiplierData.Multipliers[index];
        var color = multipliersVisualConfig.GetColor(multiplierData.Type);
        multiplier.Initialize(type, value, color);
        _multipliers.Add(multiplier);
        
        multiplierBundle.Initialize(type);
        multiplierBundle.Add(multiplier);

        spawnPosition.x += _spawnInterval;
        index++;
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