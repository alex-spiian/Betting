using UnityEngine;

public class GameFieldEdgeController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.TryGetComponent<Projectile>(out var projectile))
        {
            projectile.Hide();
        }
    }
}