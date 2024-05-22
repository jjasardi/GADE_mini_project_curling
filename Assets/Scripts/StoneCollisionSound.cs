using UnityEngine;

public class StoneCollisionSound : MonoBehaviour
{
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Stone"))
        {
            float collisionIntensity = collision.relativeVelocity.magnitude;

            float maxVolume = 1.0f;
            float volume = Mathf.Clamp(collisionIntensity / 70f, 0f, maxVolume);

            audioSource.volume = volume;
            audioSource.Play();
        }
    }
}
