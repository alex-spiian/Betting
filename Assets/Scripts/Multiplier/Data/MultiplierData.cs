using System;
using UnityEngine;

[Serializable]
public class MultiplierData
{
    [field:SerializeField] public ColorType Type { get; private set; }
    [field:SerializeField] public Transform Container { get; private set; }
    [field:SerializeField] public Transform StartSpawnPoint { get; private set; }
    [field:SerializeField] public float[] Multipliers { get; private set; }
}