using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/TargetVisualConfig", fileName = "TargetVisualConfig")]
public class MultiplierVisualConfig : ScriptableObject
{
    [SerializeField] private MultiplierVisualData[] _visualData;

    public Color GetColor(ColorType type)
    {
        var requiredData = _visualData.FirstOrDefault(container => container.Type == type);
        if (requiredData == null)
        {
            Debug.LogError($"Required Color type {type} was not found. Provide MultiplierVisualData of {type} type");
        }

        return requiredData.Color;
    }
}