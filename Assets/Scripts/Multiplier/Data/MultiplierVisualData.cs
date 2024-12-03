using System;
using UnityEngine;

[Serializable]
public class MultiplierVisualData
{
    [field:SerializeField] public ColorType Type { get; private set; }
    [field:SerializeField] public Color Color { get; private set; }

}