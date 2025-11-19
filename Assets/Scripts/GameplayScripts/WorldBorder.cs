using UnityEngine;

public class WorldBorder : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out var player))
        {
            player.Die();
        }

    }

}
