using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/PlayerInitialConfig", fileName = "PlayerInitialConfig")]
public class PlayerInitialConfig : ScriptableObject
{
        [field:SerializeField] public float Money { get; private set; }
}