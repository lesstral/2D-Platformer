using UnityEngine;

public class Collectible : MonoBehaviour
{
    private int _scoreGain = 5;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("collision");
        if (collision.gameObject.GetComponent<Player>() != null)
        {
            Debug.Log("collision player");
            Events.PlayerEvents.onCollectiblePickedUp.Publish(_scoreGain);
            Destroy(gameObject);

        }
    }


}
