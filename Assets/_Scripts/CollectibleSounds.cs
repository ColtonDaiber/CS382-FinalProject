using UnityEngine;

public class Collectible : MonoBehaviour
{
    public AudioClip[] collectSounds;  // Drag your 10 clips into this in the Inspector
    private AudioSource audioSource;

    public GameObject pickupEffect; // Optional

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (collectSounds.Length > 0 && audioSource != null)
            {
                int randomIndex = Random.Range(0, collectSounds.Length);
                AudioClip chosenClip = collectSounds[randomIndex];
                audioSource.PlayOneShot(chosenClip);
            }

            if (pickupEffect != null)
            {
                Instantiate(pickupEffect, transform.position, Quaternion.identity);
            }

            // Optional: delay destroy so sound plays
            Destroy(gameObject, 1f); // Adjust duration if clips vary
        }
    }
}
