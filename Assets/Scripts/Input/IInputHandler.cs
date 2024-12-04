using System;

public interface IInputHandler
{
    public event Action<ColorType> ProjectileSpawning;
}