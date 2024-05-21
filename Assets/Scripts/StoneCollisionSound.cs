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

            // Set the volume of the audio source based on collision intensity
            float maxVolume = 1.0f;
            float volume = Mathf.Clamp(collisionIntensity / 40f, 0f, maxVolume);

            audioSource.volume = volume;
            audioSource.Play();
        }
    }
}
