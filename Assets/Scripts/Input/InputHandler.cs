using System;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public event Action<ColorType> ProjectileSpawning;

    public void OnProjectileSpawning(string colorId)
    {
        if (Enum.TryParse(colorId, out ColorType colorType))
        {
            ProjectileSpawning?.Invoke(colorType);
            return;
        }

        Debug.LogWarning($"Can't parse {colorId} to ColorType. Add {colorId} to ColorType Enum or fix the colorID on the button.");
    }
}