using UnityEngine;

public class VictoryFlag : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Player>() != null)
        {
            Events.InGameEvents.onFlagReached.Publish();
        }
    }
}
