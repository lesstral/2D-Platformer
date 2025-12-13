using UnityEngine;
using System.Collections;

public class ShatteringBlock : MonoBehaviour
{
    [SerializeField] ParticleSystem _breakParticles;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip breakClip;
    [SerializeField] float _distance;
    [SerializeField] Vector2 _boxSize;
    [SerializeField] float _breakDelay;
    private bool _triggered;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_triggered) return;

        if (!collision.gameObject.TryGetComponent<Player>(out _))
            return;

        foreach (ContactPoint2D contact in collision.contacts)
        {
            // check if on top
            if (contact.normal.y < -0.5f)
            {
                TriggerBreak();
                break;
            }
        }
    }

    void TriggerBreak()
    {
        _triggered = true;
        StartCoroutine(BreakRoutine());
    }

    IEnumerator BreakRoutine()
    {
        yield return new WaitForSeconds(_breakDelay);

        if (audioSource && breakClip)
            audioSource.PlayOneShot(breakClip);
        GetComponent<Collider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
        _breakParticles.Play();
        Destroy(gameObject, 3f);
    }
}

