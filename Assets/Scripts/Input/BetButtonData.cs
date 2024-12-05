using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class BetButtonData
{
    [field:SerializeField] public ColorType Type { get; private set; }
    [field:SerializeField] public Button Button { get; private set; }
}