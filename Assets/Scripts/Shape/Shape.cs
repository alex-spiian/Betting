using UnityEngine;

public class Shape : MonoBehaviour
{
    [field:SerializeField] public ShapeType Type { get; private set; }
    [field:SerializeField] public MultiplierBundle[] MultiplierBundles { get; private set; }
}