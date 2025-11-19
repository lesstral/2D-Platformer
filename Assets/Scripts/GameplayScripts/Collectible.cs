using UnityEngine;

public class Collectible : MonoBehaviour
{
    [SerializeField] private int _scoreGain = 5;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("collision");
        if (collision.gameObject.GetComponent<Player>() != null)
        {
            Events.GameEvents.onCollectiblePickedUp.Publish(_scoreGain);
            Events.PlayerEvents.onPlayerActionPerformed.Publish(PlayerAction.PickUp);
            Destroy(gameObject);

        }
    }


}
